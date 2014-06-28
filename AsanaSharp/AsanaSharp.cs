using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AsanaSharp.Annotations;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace AsanaSharp
{
    public class AsanaResponse
    {
        public JObject Data { get; set; }
        public AsanaError Errors { get; set; }
    }
    public class AsanaArrayResponse
    {
        public JArray Data { get; set; }
        public AsanaError Errors { get; set; }
    }

    public class AsanaError
    {
        public string Message { get; set; }
        public string Phrase { get; set; }
    }

    internal interface IJsonLinkable
    {
        Int64 Id { get; }
    }

    public abstract class AsanaContainer : IAsanaContainer
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        [JsonIgnore]
        public Asana AsanaHost;

        [JsonIgnore]
        Asana IAsanaContainer.AsanaHost { get { return AsanaHost; } }
    }

    public interface IAsanaContainer : INotifyPropertyChanged
    {
        Asana AsanaHost { get; }
    }

    public abstract class AsanaResource : AsanaContainer, IJsonLinkable
    {
        public Int64 Id { get { return _id; } }

        [JsonProperty("id")]
        private Int64 _id;
        //Int64 IJsonLinkable.Id { get { return Id; } }


        public bool IsLocal
        {
            get { return Id == 0; }
        }

        /// <summary>
        /// Overrides the ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Id.ToString();
        }

        public static bool operator ==(AsanaResource a, Int64 id)
        {
            return a.Id == id;
        }

        public static bool operator !=(AsanaResource a, Int64 id)
        {
            return a.Id != id;
        }

        public override bool Equals(object obj)
        {
            if (obj is AsanaResource)
            {
                return Id == (obj as AsanaResource).Id;
            }
            if (obj is Int64)
            {
                return Id == (Int64)obj;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static explicit operator long(AsanaResource value)
        {
            return value.Id;
        }
    }

    public partial class Asana : AsanaContainer
    {
        public new Asana AsanaHost
        {
            get { return this; }
        }
        internal readonly RestClient RestClient;
        internal readonly JsonLinkedContext JsonCachingContext;
        internal readonly JsonSerializer JsonDeserializer; //TODO make internal
        public readonly JsonSerializer JsonSerializer; //TODO make internal
        public Asana()
        {
            RestClient = new RestClient("https://app.asana.com/api/1.0");
            //new OAuth2UriQueryParameterAuthenticator(token)
            RestClient.Authenticator = new HttpBasicAuthenticator(@"YOUR_KEY", "");

            JsonCachingContext = new JsonLinkedContext(this);

            JsonDeserializer = new JsonSerializer
            {
                //ContractResolver = new UnderscoreMappingResolver(),
                //new SelectedContractResolver(new List<string> { "id", "name", "assignee", "data", "notes", "projects", "assignee_status" }),
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Context = new StreamingContext(StreamingContextStates.All, JsonCachingContext)
                //                ReferenceResolver = refResolver
            };
            JsonDeserializer.Converters.Add(new JsonRefedConverter());

            JsonSerializer = new JsonSerializer
            {

                //ContractResolver = new SelectedContractResolver(elementsToSerialize),
                //                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize, //.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented,
                //ReferenceResolver = new AsanaReferenceResolver(JsonCachingContext),
                //Context = new StreamingContext(StreamingContextStates.All, JsonCachingContext),
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new RemoteFieldResolver(),
            };

//            var reader = new JsonTextReader(new StringReader(taskJson));
//            var jsonData = JsonDeserializer.Deserialize<AsanaResponse>(reader);
//            var jsonTask = jsonData.Data.ToObject<AsanaTask>(JsonDeserializer);
        }
    }

    internal class RemoteFieldResolver : DefaultContractResolver
    {
        /*
        public RemoteFieldResolver()
        {
            DefaultMembersSearchFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        }
        */
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var members = base.GetSerializableMembers(objectType);
            var outFields = members.Where(f => !f.Name.StartsWith("_remoteValue") && !f.Name.StartsWith("_deserialized")).ToList();
//            return members;

//            var fields = objectType.GetFields(BindingFlags.NonPublic |
//                                              BindingFlags.Instance).Cast<MemberInfo>();
//            var outFields = fields.Where(f => f.Name.StartsWith("_remote")).ToList();
            return outFields;
            //                return base.GetSerializableMembers(objectType);
        }
        /*
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            // only serializer properties that were specified
            //            if (_list.ContainsKey(type))
            //                properties = properties.Where(p => _list[type].Contains(p.PropertyName)).ToList();
//            foreach (var prop in properties)
//            {
//                prop.ShouldSerialize = o => true;
//            }
//            properties.Select(p => p.ShouldSerialize = o => true);
            return properties;
        }
        */
    }
    public class UnderscoreMappingResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return System.Text.RegularExpressions.Regex.Replace(
                propertyName, @"([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", "$1$3_$2$4").ToLower();
        }
    }

    public class SelectedContractResolver : UnderscoreMappingResolver
    {
//            private readonly List<string> _list;
//            public SelectedContractResolver(List<string> list)
//            {
//                _list = list;
//            }
        private readonly Dictionary<Type, List<string>> _list;
        public SelectedContractResolver(Dictionary<Type, List<string>> list)
        {
            _list = list;
        }
        /*
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            return property;
        }
        */

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
        
            // only serializer properties that were specified
            if (_list.ContainsKey(type))
                properties = properties.Where(p => _list[type].Contains(p.PropertyName)).ToList();
        
            return properties;
        }
    }


    // http://stackoverflow.com/questions/13108693/serializing-heavily-linked-data-in-net-customizing-json-net-references/13113901#13113901
    class JsonRefConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((IJsonLinkable)value).Id);
        }
        /*
        public override object ReadJson(JsonReader reader, Type type, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var value = JsonLinkedContext.GetLinkedValue(serializer, type, (Int64)jo.Property("id")); //.PropertyValues().First()
            serializer.Populate(jo.CreateReader(), value);
            return value;
        }
        */
        
        public override object ReadJson(JsonReader reader, Type type, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                //                var outObj = Activator.CreateInstance(type, true);
                //                var outObj = serializer.Deserialize(reader, type);
                //                return JsonLinkedContext.GetLinkedValue(serializer, type, ((IJsonLinkable)outObj).Id);
                return JsonLinkedContext.SetLinkedValue(serializer, type, reader);
            }
            else
                    throw new Exception("Ref value must be an object.");
//                        if (reader.TokenType != JsonToken.String)
//                    throw new Exception("Ref value must be a string.");
//                return JsonLinkedContext.GetLinkedValue(serializer, type, reader.Value.ToString());
        }
        
        public override bool CanConvert(Type type)
        {
            return type.IsAssignableFrom(typeof(IJsonLinkable));
        }
    }

    class JsonRefedConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //var test = serializer.ReferenceLoopHandling;
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type type, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var value = JsonLinkedContext.GetLinkedValue(serializer, type, (Int64)jo.Property("id")); //.PropertyValues().First()
            serializer.Populate(jo.CreateReader(), value);
            return value;
        }

        public override bool CanConvert(Type type)
        {
            //return type.IsAssignableFrom(typeof(IJsonLinkable));
            return typeof(IJsonLinkable).IsAssignableFrom(type);
        }
    }

    public class JsonLinkedContext //make internal
    {
        private readonly IDictionary<Type, IDictionary<Int64, object>> links = new Dictionary<Type, IDictionary<Int64, object>>();
        public readonly Asana AsanaHost;

        public JsonLinkedContext(Asana asanaHost)
        {
            AsanaHost = asanaHost;
        }

        public object GetValueById(Int64 reference)
        {
            object value = null;
            foreach (var link in links.Values)
            {
                if (link.TryGetValue(reference, out value))
                    break;
            }
            return value;
        }

        public bool ContainsId(Int64 reference)
        {
            return links.Values.Any(link => link.ContainsKey(reference));
        }

        public static object GetLinkedValue(JsonSerializer serializer, Type type, Int64 reference)
        {
            var context = (JsonLinkedContext)serializer.Context.Context;
            IDictionary<Int64, object> links;
            if (!context.links.TryGetValue(type, out links))
                context.links[type] = links = new Dictionary<Int64, object>();
            object value;
            if (!links.TryGetValue(reference, out value))
                links[reference] = value = FormatterServices.GetUninitializedObject(type);
            else
            {
                    
            }
            return value;
        }

        public static object SetLinkedValue(JsonSerializer serializer, Type type, JsonReader reader)
        {
            var refObj = serializer.Deserialize(reader, type);
            var reference = ((IJsonLinkable)refObj).Id;
            var context = (JsonLinkedContext)serializer.Context.Context;
            IDictionary<Int64, object> links;
            if (!context.links.TryGetValue(type, out links))
                context.links[type] = links = new Dictionary<Int64, object>();
            object value;
            if (!links.TryGetValue(reference, out value))
                links[reference] = value = refObj;
            else
                serializer.Populate(reader, value);
            return value;
        }
    }

    public class AsanaReferenceResolver : IReferenceResolver
    {
        private readonly JsonLinkedContext JsonLinkedContext;
        //private readonly IDictionary<Int64, ObjectId> _people = new Dictionary<Int64, ObjectId>();

        public AsanaReferenceResolver(JsonLinkedContext context)
        {
            JsonLinkedContext = context;
        }

        public object ResolveReference(object context, string reference)
        {
//            var jsonContext = (JsonLinkedContext)context;
            var id = Int64.Parse(reference);

            var p = JsonLinkedContext.GetValueById(id);

            return p;
        }

        public string GetReference(object context, object value)
        {
//            var jsonContext = (JsonLinkedContext)context;
            var p = (IJsonLinkable)value;
            //_referencedOnce.Add(p.Id);

            return p.Id.ToString();
        }

        //private List<long> _referencedOnce = new List<long>();
        private Dictionary<long, int> _referencedCount = new Dictionary<long, int>();


        public bool IsReferenced(object context, object value)
        {
//            var jsonContext = (JsonLinkedContext)context;
            var p = (IJsonLinkable)value;
            if (JsonLinkedContext.ContainsId(p.Id))
            {
                return true;
                if (_referencedCount.ContainsKey(p.Id))
                {
                    _referencedCount[p.Id]++;
                    if (_referencedCount[p.Id] >= 2)
                        return true;
                }
                else
                {
                    _referencedCount.Add(p.Id, 1);
                }
            }
            return false;
        }

        public void AddReference(object context, string reference, object value)
        {
            Int64 id = Int64.Parse(reference);
            var outValue = (IJsonLinkable)value;
//            _people[id] = outValue;
//            outValue.Id = id;
        }
    }

}
