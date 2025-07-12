using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.ServiceModel.Channels;

namespace ec.edu.monster.controlador
{
    // Atributo personalizado para usar en tus métodos del controlador
    public class JsonDateFormatAttribute : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription,
            ClientOperation clientOperation)
        {
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription,
            DispatchOperation dispatchOperation)
        {
            dispatchOperation.Formatter = new JsonDateFormatter(dispatchOperation.Formatter);
        }

        public void Validate(OperationDescription operationDescription)
        {
        }
    }

    // Formateador personalizado que usa Newtonsoft.Json
    public class JsonDateFormatter : IDispatchMessageFormatter
    {
        private IDispatchMessageFormatter _innerFormatter;

        public JsonDateFormatter(IDispatchMessageFormatter innerFormatter)
        {
            _innerFormatter = innerFormatter;
        }

        public void DeserializeRequest(Message message, object[] parameters)
        {
            _innerFormatter.DeserializeRequest(message, parameters);
        }

        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            if (result != null)
            {
                // Serializar usando Newtonsoft.Json con formato de fecha ISO
                var settings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    Formatting = Formatting.None,
                    NullValueHandling = NullValueHandling.Ignore
                };

                string json = JsonConvert.SerializeObject(result, settings);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

                Message reply = Message.CreateMessage(messageVersion, "",
                    new JsonBodyWriter(jsonBytes));

                reply.Properties.Add(WebBodyFormatMessageProperty.Name,
                    new WebBodyFormatMessageProperty(WebContentFormat.Raw));

                return reply;
            }

            return _innerFormatter.SerializeReply(messageVersion, parameters, result);
        }
    }

    // Writer para el cuerpo del mensaje JSON
    public class JsonBodyWriter : BodyWriter
    {
        private byte[] _jsonBytes;

        public JsonBodyWriter(byte[] jsonBytes) : base(false)
        {
            _jsonBytes = jsonBytes;
        }

        protected override void OnWriteBodyContents(System.Xml.XmlDictionaryWriter writer)
        {
            writer.WriteStartElement("Binary");
            writer.WriteBase64(_jsonBytes, 0, _jsonBytes.Length);
            writer.WriteEndElement();
        }
    }
}