//----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//----------------------------------------------------------------

using System.ServiceModel.Channels;

namespace imL.Package.ServiceModel
{
    public class CustomTextMessageEncoderFactory : MessageEncoderFactory
    {
        readonly MessageEncoder encoder;
        readonly MessageVersion version;
        readonly string mediaType;
        readonly string charSet;

        internal CustomTextMessageEncoderFactory(string mediaType, string charSet,
            MessageVersion version)
        {
            version = version;
            mediaType = mediaType;
            charSet = charSet;
            encoder = new CustomTextMessageEncoder(this);
        }

        public override MessageEncoder Encoder
        {
            get
            {
                return encoder;
            }
        }

        public override MessageVersion MessageVersion
        {
            get
            {
                return version;
            }
        }

        internal string MediaType
        {
            get
            {
                return mediaType;
            }
        }

        internal string CharSet
        {
            get
            {
                return charSet;
            }
        }
    }
}
