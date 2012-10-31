using System;
using System.Runtime.Serialization;

namespace Demo.RabbitMQ.Serializers
{
    public interface ISerializer<TMessage>
    {
        byte[] Serialize(TMessage message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        /// <exceptions>
        ///		<exception cref="ArgumentNullException">The serializationStream is null.</exception>
        ///     <exception cref="SerializationException">The serializationStream supports seeking, but its length is 0. -or-The target type is a System.Decimal, but the value is out of range of the System.Decimal type.</exception>
        ///		<exception cref="InvalidCastException">Unable to cast object to type of TMessage.</exception>>
        /// </exceptions>
        TMessage DeSerialize(byte[] bytes);
    }
}
