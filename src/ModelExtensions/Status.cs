using System;
using System.Xml.Serialization;

namespace Statnett.EdxLib.ModelExtensions
{
    [Serializable]
    public enum Status
    {
        Unknown,
        [XmlEnum("ACCEPTED")]
        Accepted,
        [XmlEnum("SENT")]
        Sent,
        [XmlEnum("SUCCESSFULLY_SENT")]
        SuccessfullySent,
        [XmlEnum("DELIVERED")]
        Delivered,
        [XmlEnum("RECEIVED")]
        Received,
        [XmlEnum("FAILED")]
        Failed
    }
}