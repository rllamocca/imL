﻿using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace imL.Package.ServiceModel
{
    public class CustomTextMessageBindingElement : MessageEncodingBindingElement
    {
        MessageVersion msgVersion;
        string mediaType;
        string encoding;
        readonly XmlDictionaryReaderQuotas readerQuotas;

        CustomTextMessageBindingElement(CustomTextMessageBindingElement binding)
            : this(binding.Encoding, binding.MediaType, binding.MessageVersion)
        {
            readerQuotas = new XmlDictionaryReaderQuotas();
            binding.ReaderQuotas.CopyTo(readerQuotas);
        }

        public CustomTextMessageBindingElement(string encoding, string mediaType,
            MessageVersion msgVersion)
        {
            msgVersion = msgVersion ?? throw new ArgumentNullException(nameof(msgVersion));
            mediaType = mediaType ?? throw new ArgumentNullException(nameof(mediaType));
            encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            readerQuotas = new XmlDictionaryReaderQuotas();
        }

        public CustomTextMessageBindingElement(string encoding, string mediaType)
            : this(encoding, mediaType, MessageVersion.Soap11WSAddressingAugust2004)
        {
        }

        public CustomTextMessageBindingElement(string encoding)
            : this(encoding, "text/xml")
        {

        }

        public CustomTextMessageBindingElement()
            : this("UTF-8")
        {
        }


        public override MessageVersion MessageVersion
        {
            get
            {
                return msgVersion;
            }

            set
            {
                msgVersion = value ?? throw new ArgumentNullException(nameof(value));
            }
        }


        public string MediaType
        {
            get
            {
                return mediaType;
            }

            set
            {
                mediaType = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public string Encoding
        {
            get
            {
                return encoding;
            }

            set
            {
                encoding = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        // This encoder does not enforces any quotas for the unsecure messages. The  
        // quotas are enforced for the secure portions of messages when this encoder 
        // is used in a binding that is configured with security.  
        public XmlDictionaryReaderQuotas ReaderQuotas
        {
            get
            {
                return readerQuotas;
            }
        }

        #region IMessageEncodingBindingElement Members 

        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new CustomTextMessageEncoderFactory(MediaType,
                Encoding, MessageVersion);
        }

        #endregion


        public override BindingElement Clone()
        {
            return new CustomTextMessageBindingElement(this);
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return context.CanBuildInnerChannelFactory<TChannel>();
        }

        /* public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context) 
         { 
             if (context == null) 
                 throw new ArgumentNullException("context"); 

             context.BindingParameters.Add(this); 
             return context.BuildInnerChannelListener<TChannel>(); 
         } 

         public override bool CanBuildChannelListener<TChannel>(BindingContext context) 
         { 
             if (context == null) 
                 throw new ArgumentNullException("context"); 

             context.BindingParameters.Add(this); 
             return context.CanBuildInnerChannelListener<TChannel>(); 
         } */

        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
            {
                return (T)(object)readerQuotas;
            }
            else
            {
                return base.GetProperty<T>(context);
            }
        }

        #region IWsdlExportExtension Members 



        #endregion
    }

}
