using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace AsanaSharp
{
    public static class JTokenExtensions
    {
        public static JToken DeserializeAndCombineDuplicates(JsonTextReader reader)
        {
            if (reader.TokenType == JsonToken.None)
            {
                reader.Read();
            }

            if (reader.TokenType == JsonToken.StartObject)
            {
                reader.Read();
                JObject obj = new JObject();
                while (reader.TokenType != JsonToken.EndObject)
                {
                    string propName = (string)reader.Value;
                    reader.Read();
                    JToken newValue = DeserializeAndCombineDuplicates(reader);

                    JToken existingValue = obj[propName];
                    if (existingValue == null)
                    {
                        obj.Add(new JProperty(propName, newValue));
                    }
                    else if (existingValue.Type == JTokenType.Array)
                    {
                        CombineWithArray((JArray)existingValue, newValue);
                    }
                    else // Convert existing non-array property value to an array
                    {
                        JProperty prop = (JProperty)existingValue.Parent;
                        JArray array = new JArray();
                        prop.Value = array;
                        array.Add(existingValue);
                        CombineWithArray(array, newValue);
                    }

                    reader.Read();
                }
                return obj;
            }

            if (reader.TokenType == JsonToken.StartArray)
            {
                reader.Read();
                JArray array = new JArray();
                while (reader.TokenType != JsonToken.EndArray)
                {
                    array.Add(DeserializeAndCombineDuplicates(reader));
                    reader.Read();
                }
                return array;
            }

            return new JValue(reader.Value);
        }

        private static void CombineWithArray(JArray array, JToken value)
        {
            if (value.Type == JTokenType.Array)
            {
                foreach (JToken child in value.Children())
                    array.Add(child);
            }
            else
            {
                array.Add(value);
            }
        }

    }

    internal class AsanaListConverter<TCollection, TInner> : CustomCreationConverter<TCollection> where TCollection : new() //JsonConverter where T : new() //
    {
        //protected T Create(Type objectType, JObject jObject);

        public override TCollection Create(Type objectType)
        {
            return new TCollection();
        }

        //        public override bool CanConvert(Type objectType)
        //        {
        //            return typeof(T).IsAssignableFrom(objectType);
        //        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //            if (!ReferenceEquals(existingValue, null))
            //                Console.WriteLine("wtf");

            var output = Create(objectType);
            var outputAsAsanaCollection = output as AsanaReadOnlyCollection<TInner>;
            var deserializedList = new List<TInner>();
            serializer.Populate(reader, deserializedList);
            outputAsAsanaCollection.Import(deserializedList);
            return output;
            //return base.ReadJson(reader, objectType, existingValue, serializer);
        }

    }

    /// <summary>
    /// Special JsonConvert resolver that allows you to ignore properties.  See http://stackoverflow.com/a/13588192/1037948
    /// </summary>
    public class IgnorableSerializerContractResolver : DefaultContractResolver
    {
        protected readonly Dictionary<Type, HashSet<string>> Ignores;

        public IgnorableSerializerContractResolver()
        {
            this.Ignores = new Dictionary<Type, HashSet<string>>();
        }

        public IgnorableSerializerContractResolver Ignore<TModel>(Expression<Func<TModel, object>> selector)
        {
            MemberExpression body = selector.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)selector.Body;
                body = ubody.Operand as MemberExpression;

                if (body == null)
                {
                    throw new ArgumentException("Could not get property name", "selector");
                }
            }

            string propertyName = body.Member.Name;
            this.Ignore(typeof(TModel), propertyName);
            return this;
        }


        /// <summary>
        /// Explicitly ignore the given property(s) for the given type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName">one or more properties to ignore.  Leave empty to ignore the type entirely.</param>
        public void Ignore(Type type, params string[] propertyName)
        {
            // start bucket if DNE
            if (!this.Ignores.ContainsKey(type)) this.Ignores[type] = new HashSet<string>();

            foreach (var prop in propertyName)
            {
                this.Ignores[type].Add(prop);
            }
        }

        /// <summary>
        /// Is the given property for the given type ignored?
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public bool IsIgnored(Type type, string propertyName)
        {
            if (!this.Ignores.ContainsKey(type)) return false;

            // if no properties provided, ignore the type entirely
            if (this.Ignores[type].Count == 0) return true;

            return this.Ignores[type].Contains(propertyName);
        }

        /// <summary>
        /// The decision logic goes here
        /// </summary>
        /// <param name="member"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (this.IsIgnored(property.DeclaringType, property.PropertyName)
    // need to check basetype as well for EF -- @per comment by user576838
    || this.IsIgnored(property.DeclaringType.BaseType, property.PropertyName))
            {
                property.ShouldSerialize = instance => { return false; };
            }

            return property;
        }
    }

    class Unused
    {
        public class AsanaSharp
        {



            public void Deserialize()
            {
                var taskJson =
                    "{\r\n  \"data\": {\r\n    \"assignee\": {\r\n      \"id\": 1235,\r\n      \"name\": \"Tim Bizarro\"\r\n    },\r\n    \"assignee_status\": \"inbox\",\r\n    \"completed\": false,\r\n    \"completed_at\": null,\r\n    \"created_at\": \"2012-02-22T02:06:58.158Z\",\r\n    \"due_on\": null,\r\n    \"followers\": [\r\n      {\r\n        \"id\": 5678,\r\n        \"name\": \"Greg Sanchez\"\r\n      }\r\n    ],\r\n    \"id\": 1001,\r\n    \"modified_at\": \"2012-02-22T02:06:58.158Z\",\r\n    \"name\": \"Hello, world!\",\r\n    \"notes\": \"How are you today?\",\r\n    \"parent\": null,\r\n    \"projects\": [\r\n      {\r\n        \"id\": 14641,\r\n        \"name\": \"Cat Stuff\"\r\n      }\r\n    ],\r\n    \"workspace\": {\r\n      \"id\": 14916,\r\n      \"name\": \"My Favorite Workspace\"\r\n    }\r\n  }\r\n}";

                var cachingContext = new JsonLinkedContext(new Asana());

                var serializer = new JsonSerializer
                {
                    ContractResolver = new UnderscoreMappingResolver(),
                    //new SelectedContractResolver(new List<string> { "id", "name", "assignee", "data", "notes", "projects", "assignee_status" }),
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Context = new StreamingContext(StreamingContextStates.All, cachingContext),

                    //                ReferenceResolver = refResolver
                };

                var reader = new JsonTextReader(new StringReader(taskJson));

                var jsonData = serializer.Deserialize<AsanaResponse>(reader);
                var jsonTask = jsonData.Data.ToObject<AsanaTask>(serializer);
                //            var jsonTask2 = serializer.Populate()
                var jsonTask2 = new AsanaTask();

                serializer.Populate(jsonData.Data.CreateReader(), jsonTask2);

                // when not provided, serializes everything
                var elementsToSerialize = new Dictionary<Type, List<string>>
            {
                {typeof (AsanaTask), new List<string> {"id", "name", "assignee", "assignee_status"}},
                {typeof (AsanaProject), new List<string> {"id", "name", "notes"}},
                {typeof (AsanaUser), new List<string> {"name"}}
            };

                var serialized = JsonConvert.SerializeObject(jsonTask, Formatting.Indented, new JsonSerializerSettings
                {
                    ContractResolver = new SelectedContractResolver(elementsToSerialize),
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Context = new StreamingContext(StreamingContextStates.All, new JsonLinkedContext(new Asana())),
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
        }

    }

    /*
    [JsonObject(MemberSerialization.OptIn)]
    class Family
    {
        [JsonProperty(ItemConverterType = typeof(JsonRefedConverter))]
        public List<Person> persons;
    }

    [JsonObject(MemberSerialization.OptIn)]
    class Person : JsonLinkable
    {
        [JsonProperty]
        public string name;

        [JsonProperty, JsonConverter(typeof(JsonRefConverter))]
        public Person mate;

        [JsonProperty(ItemConverterType = typeof(JsonRefConverter))]
        public List<Person> children;

    }
    */


    public class ObjectId
    {
        public Int64 Id { get; set; }
    }

    public class PersonReference : ObjectId
    {
        public string Name { get; set; }
        public PersonReference Spouse { get; set; }
        public List<PersonReference> Children { get; set; }
        public List<ToolsReference> Tools { get; set; }
    }

    public class ToolsReference : ObjectId
    {
        public string Name { get; set; }
    }
    public class IdReferenceResolver : IReferenceResolver
    {
        private readonly IDictionary<Int64, ObjectId> _people = new Dictionary<Int64, ObjectId>();

        public object ResolveReference(object context, string reference)
        {
            Int64 id = Int64.Parse(reference);

            ObjectId p;
            _people.TryGetValue(id, out p);

            return p;
        }

        public string GetReference(object context, object value)
        {
            ObjectId p = (ObjectId)value;
            _people[p.Id] = p;

            return p.Id.ToString();
        }

        public bool IsReferenced(object context, object value)
        {
            ObjectId p = (ObjectId)value;

            return _people.ContainsKey(p.Id);
        }

        public void AddReference(object context, string reference, object value)
        {
            Int64 id = Int64.Parse(reference);
            var outValue = (ObjectId)value;
            _people[id] = outValue;
            outValue.Id = id;
        }
    }
    /*
    public void DeserializeCustomReferenceResolver()
    {
        string json = @"[
{
""$id"": ""41414"",
""Name"": ""John Smith1"",
""Spouse"": {
  ""$id"": ""99898989"",
  ""Name"": ""Jane Smith"",
  ""Spouse"": {
    ""$ref"": ""41414""
  }
}
},
{
""$ref"": ""99898989""
}
]";

        IList<PersonReference> people = JsonConvert.DeserializeObject<IList<PersonReference>>(json, new JsonSerializerSettings
        {
            ReferenceResolver = new IdReferenceResolver(),
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            Formatting = Formatting.Indented
        });

        PersonReference john = people[0];
        PersonReference jane = people[1];

        string jsonReserialize = JsonConvert.SerializeObject(people, new JsonSerializerSettings
        {
            ReferenceResolver = new IdReferenceResolver(),
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            Formatting = Formatting.Indented
        });
    }

    public void SerializeCustomReferenceResolver()
    {
        PersonReference john = new PersonReference
        {
            Id = 1234,
            Name = "John Smith",
            Children = new List<PersonReference>(),
            Tools = new List<ToolsReference>()
        };

        PersonReference jane = new PersonReference
        {
            Id = 5678,
            Name = "Jane Smith"
        };

        PersonReference child = new PersonReference
        {
            Id = 9999,
            Name = "Child Smith",
            Tools = new List<ToolsReference>()
        };

        ToolsReference puppet = new ToolsReference
        {
            Id = 888888,
            Name = "Book"
        };

        john.Spouse = jane;
        jane.Spouse = john;
        john.Children.Add(child);
        john.Tools.Add(puppet);
        child.Tools.Add(puppet);

        IList<PersonReference> people = new List<PersonReference>
        {
            john,
            jane
        };

        string json = JsonConvert.SerializeObject(people, new JsonSerializerSettings
        {
            ReferenceResolver = new IdReferenceResolver(),
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            Formatting = Formatting.Indented
        });
    }
    */
    //}

    /*
    public class Program
    {
        public static void Main()
        {
            String s = "{\n" +
" \"persons\": [\n" +
" {\n" +
" \"name\": \"mom\",\n" +
" \"mate\": {\"name\": \"dad1\", " +
            "\"children\": [\"sis-mate\"] },\n" +
" \"children\": [\n" +
" \"bro\",\n" +
" \"sis\"\n" +
" ]\n" +
" },\n" +
" {\n" +
" \"name\": \"dad1\",\n" +
" \"mate\": \"mom\",\n" +
" \"children\": [\n" +
" \"bro\",\n" +
" \"sis\"\n" +
" ]\n" +
" },\n" +
" {\n" +
" \"name\": \"bro\",\n" +
" \"mate\": \"bro-mate\"\n" +
" },\n" +
" {\n" +
" \"name\": \"sis\",\n" +
" \"mate\": { \"name\": \"sis-mate\" }\n" +
" }\n" +
" ]\n" +
"}";

            var obj = JsonConvert.DeserializeObject<Family>(s, new JsonSerializerSettings
            {
                Context = new StreamingContext(StreamingContextStates.All, new JsonLinkedContext()),
                
            });
        }
    }
    */

    static class JsonExtension
    {
        public static void MergeInto(
    this JContainer left, JToken right)
        {
            foreach (var rightChild in right.Children<JProperty>())
            {
                var rightChildProperty = rightChild;
                var leftProperty = left.SelectToken(rightChildProperty.Name);

                if (leftProperty == null)
                {
                    // no matching property, just add 
                    left.Add(rightChild);
                }
                else
                {
                    var leftObject = leftProperty as JObject;
                    if (leftObject == null)
                    {
                        // replace value
                        var leftParent = (JProperty)leftProperty.Parent;
                        leftParent.Value = rightChildProperty.Value;
                    }

                    else
                        // recurse object
                        MergeInto(leftObject, rightChildProperty.Value);
                }
            }
        }
    }
    /*

    class ItemConverter : CustomCreationConverter<Item>
    {
        readonly IEnumerable<Category> _repository;

        public ItemConverter(IEnumerable<Category> categories)
        {
            _repository = categories;
        }

        public override Item Create(Type objectType)
        {
            return new Item();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            int categoryId = jObject["categoryId"].ToObject<int>();
            Category category = _repository.SingleOrDefault(x => x.CategoryId == categoryId);

            Item result = (Item)base.ReadJson(jObject.CreateReader(), objectType, existingValue, serializer);
            result.Category = category;

            return result;
        }
    }

    class Item
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        public Category Category { get; set; }
        // other properties.
    }

    class Category
    {
        [JsonProperty("id")]
        public int CategoryId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        // other properties.
    }

    public class Program
    {
        public static void Main()
        {
            // sample : json contains items and/or categories in an array.
            string jsonString = @"
                [ 
                        {
                                        ""id"" : ""2"",
                                ""categoryId"" : ""35"",
                                      ""type"" : ""item"",
                                      ""name"" : ""hamburger""
                        },
                        {
                                        ""id"" : ""35"",
                                      ""type"" : ""category"",
                                      ""name"" : ""drinks"" 
                        }
                ]";

            JArray jsonArray = JArray.Parse(jsonString);

            // Separate between category and item data.
            IEnumerable<JToken> jsonCategories = jsonArray.Where(x => x["type"].ToObject<string>() == "category");
            IEnumerable<JToken> jsonItems = jsonArray.Where(x => x["type"].ToObject<string>() == "item");

            // Create list of category from jsonCategories.
            IEnumerable<Category> categories = jsonCategories.Select(x => x.ToObject<Category>());

            // Settings for jsonItems deserialization.
            JsonSerializerSettings itemDeserializerSettings = new JsonSerializerSettings();
            itemDeserializerSettings.Converters.Add(new ItemConverter(categories));
            JsonSerializer itemDeserializer = JsonSerializer.Create(itemDeserializerSettings);

            // Create list of item from jsonItems.
            IEnumerable<Item> items = jsonItems.Select(x => x.ToObject<Item>(itemDeserializer));
        }
    }
    */
}
