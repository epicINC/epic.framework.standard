using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Socket.IO
{

    public enum PacketType
    {
        Connect,
        Disconnect,
        Event,
        Ack,
        Error,
        BinaryEvent,
        BinaryACK
    }


    internal static class Packet
    {
        public static Packet<T> Create<T>(PacketType type, T data)
        {
            return new Packet<T>() { Type = type, Data = data };
        }



        public static string EncodeError(string value)
        {
            return $"{(int)PacketType.Error}{value}";
        }

        public static Packet<string> DecodeError(string value)
        {
            return Create(PacketType.Error, value);


        }
    }

    public class Packet<T>
    {

        public int? ID { get; set; }
        public string NSP { get; set; }
        public PacketType Type { get; set; }
        public T Data { get; set; }


    }

    public class Parser
    {


        public static string Encode<T>(Packet<T> packet)
        {
            return EncodeAsString(packet);

        }

        static string EncodeAsString<T>(Packet<T> packet)
        {
            var result = packet.Type.ToString();

            if (String.IsNullOrWhiteSpace(packet.NSP) || packet.NSP == "/")
                result += packet.NSP + ",";

            if (packet.ID.HasValue)
                result += packet.ID.Value.ToString();

            if (packet.Data == null) return result;

            var payload = JSON.Stringify(packet.Data);
            if (payload == null) return Packet.EncodeError("encode error");

            return result + payload;
        }


        public Packet<string> Decode(string value) 
        {
            var offset = 0;
            var result = new Packet<string>()
            {
                Type = (PacketType)(value[offset++] - 48)
            };

            if (Enum.IsDefined(typeof(PacketType), result.Type)) return Packet.DecodeError($"unknown packet type {result.Type}");


            if (value[offset++] == '/')
            {
                result.NSP = String.Empty;

            }


            return result;

        }
    }
}
