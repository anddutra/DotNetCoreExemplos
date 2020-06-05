using Google.Cloud.Firestore;
using System;
using System.Collections.Concurrent;

namespace FireBaseExemplos.Util
{
    public class EnumFirestoreConverter<T> : IFirestoreConverter<T> where T : struct
    {
        private static readonly ConcurrentDictionary<string, T> _dictionary = new ConcurrentDictionary<string, T>();
        public T FromFirestore(object value)
        {
            string valueString = (string)value;

            if (_dictionary.TryGetValue(valueString, out T result))
                return result;
            else
            {
                try
                {
                    result = (T)ExtractAttributeEnum.GetEnumFromValue<T>(value);
                    _dictionary.TryAdd(valueString, result);
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Value for {valueString} not mapped on enum {nameof(T)}");
                }
            }
        }

        public object ToFirestore(T value)
        {
            return ExtractAttributeEnum.GetEnumValue<T>(value) ?? throw new Exception($"Match not found for value {value} on enum {nameof(T)}");
        }
    }
}
