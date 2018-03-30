﻿using PrefsWrapper.Encoders;
using RuntimeUnitTestToolkit;
using UnityEngine;
using System;

namespace PrefsWrapper.Serializers
{
    public class SerializerTest
    {
        public void SerializeString()
        {
            var key = "test-string";
            var value = "test";
            var serializer = new StringPrefSerializer();
            serializer.Serialize(key, value);
            serializer.Deserialize(key).Is(value);
            PlayerPrefs.HasKey(key).IsTrue();
            PlayerPrefs.GetString(key).Is(value);
        }

        public void SerializeInt()
        {
            var key = "test-int";
            var value = 1;
            var serializer = new IntPrefSerializer();
            serializer.Serialize(key, value);
            serializer.Deserialize(key).Is(value);
            PlayerPrefs.HasKey(key).IsTrue();
            PlayerPrefs.GetInt(key).Is(value);
        }

        public void SerializeFloat()
        {
            var key = "test-float";
            var value = 1f;
            var serializer = new FloatPrefSerializer();
            serializer.Serialize(key, value);
            serializer.Deserialize(key).Is(value);
            PlayerPrefs.HasKey(key).IsTrue();
            PlayerPrefs.GetFloat(key).Is(value);
        }

        public void SerializeBool()
        {
            var key = "test-bool";
            var value = true;
            var serializer = new BoolPrefSerializer();
            serializer.Serialize(key, value);
            serializer.Deserialize(key).Is(value);
            PlayerPrefs.HasKey(key).IsTrue();
            PlayerPrefs.GetInt(key).Is(1);
        }

        public void SerializeBinary()
        {
            var key = "test-binary";
            var value = new byte[] { 0x00, 0x01 };
            var serializer = new BinaryPrefSerializer();
            serializer.Serialize(key, value);
            serializer.Deserialize(key).IsCollection(value);
            PlayerPrefs.HasKey(key).IsTrue();
            PlayerPrefs.GetString(key).Is("AAE=");
        }

        public void SerializeJson()
        {
            var key = "test-json";
            var value = Vector3.up;
            var serializer = new JsonPrefSerializer<Vector3>();
            serializer.Serialize(key, value);
            serializer.Deserialize(key).Is(value);
            PlayerPrefs.HasKey(key).IsTrue();
            PlayerPrefs.GetString(key).Is(JsonUtility.ToJson(Vector3.up));
        }

        public void SerializeEnum()
        {
            var key = "test-enum";
            var value = TestType.Fuga;
            var serializer = new EnumPrefSerializer<TestType>();
            serializer.Serialize(key, value);
            serializer.Deserialize(key).Is(value);
            PlayerPrefs.HasKey(key).IsTrue();
            PlayerPrefs.GetString(key).Is(TestType.Fuga.ToString());
        }

        public enum TestType { Hoge, Fuga, Piyo }

        public void SerializeDateTime()
        {
            var key = "test-datetime";
            var value = DateTime.Now;
            var serializer = new DateTimePrefSerializer();
            serializer.Serialize(key, value);
            serializer.Deserialize(key).Is(value);
            PlayerPrefs.HasKey(key).IsTrue();
            PlayerPrefs.GetString(key).Is(value.ToBinary().ToString());
        }

        public void SerializeTimeSpan()
        {
            var key = "test-timespan";
            var value = TimeSpan.FromSeconds(1);
            var serializer = new TimeSpanPrefSerializer();
            serializer.Serialize(key, value);
            serializer.Deserialize(key).Is(value);
            PlayerPrefs.HasKey(key).IsTrue();
            PlayerPrefs.GetString(key).Is(value.Ticks.ToString());
        }

        public void EncodeSerialize()
        {
            var key = "test-encode";
            var value = 1;
            var intEncoder = new IntEncoder();
            var serializer = new EncodeSerializer<int>(new BinaryPrefSerializer(), intEncoder);
            serializer.Serialize(key, value);
            serializer.Deserialize(key).Is(value);
            PlayerPrefs.HasKey(key).IsTrue();
            PlayerPrefs.GetString(key).Is("AQAAAA==");
        }
    }
}