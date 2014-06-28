using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace AsanaSharp
{

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)] //.Property
    public class EndpointAttribute : Attribute
    {
        public readonly string Endpoint;
        public readonly EndpointMethods Methods;
        public readonly Type[] ParameterTypes;
        public EndpointAttribute(string endpoint, EndpointMethods methods = EndpointMethods.Get, params Type[] parameterTypes)
        {
            Endpoint = endpoint;
            Methods = methods;
            ParameterTypes = parameterTypes ?? new Type[] { };
        }
    }

    public enum ParamType
    {
        Internal,
        //UrlSegment,
        Required,
        Optional
    }

    public abstract class ParamAttribute : Attribute
    {
        public readonly ParamType ParamType;
        public readonly string ParamName;
        public readonly Type Type;
        public readonly string DefaultValue;
        public readonly string InternalMemberName;
        public readonly string InnerProperty;

        public string RealInternalMemberName
        {
            get { return ConvertUnderscoreToCapitals(InternalMemberName); }
        }

        public string RealInnerProperty
        {
            get { return ConvertUnderscoreToCapitals(InnerProperty); }
        }
        public string FullInnerProperty
        {
            get
            {
                if (!String.IsNullOrEmpty(InnerProperty))
                {
                    return NiceParamName + '.' + RealInnerProperty;
                }
                else return null;
            }
        }

        public string NiceDefaultValue
        {
            get { return ConvertUnderscoreToCapitals(DefaultValue); }
        }

        public string NiceDefaultValueParamName
        {
            get { return String.Format("{0}Is{1}", NiceParamName, NiceDefaultValue); }
        }

        public string NiceParamName
        {
            get
            {
                var niceParam = ConvertUnderscoreToCapitals(ParamName);
                return Char.ToLowerInvariant(niceParam[0]) + niceParam.Substring(1);
            }
        }

        public string PropertyParamName
        {
            get
            {
                var realParam = ConvertUnderscoreToCapitals(ParamName);
                return realParam;
            }
        }

        public static string ConvertUnderscoreToCapitals(string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            var str = Regex.Replace(
                    input, @"([a-zA-Z0-9]+)[_.]|([a-zA-Z0-9]+)",
                m => Char.ToUpperInvariant(m.ToString()[0]) + m.ToString().Substring(1));
            return str.Replace("_", String.Empty); // first letter
        }

        public ParamAttribute(ParamType paramType, string param, Type type, string defaultValue = null, string internalMemberReference = null, string innerProperty = null)
        {
            ParamType = paramType;
            ParamName = param;
            Type = type;
            DefaultValue = defaultValue;
            InternalMemberName = internalMemberReference;
            InnerProperty = innerProperty;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class OnCreateSetForeignPropertyInside : Attribute
    {
        public string InFetchedObjectPropertyName;
        public string ClassReferenceObject;
        public OnCreateSetForeignPropertyInside(string inFetchedObjectPropertyName, string classReferenceObject)
        {
            InFetchedObjectPropertyName = inFetchedObjectPropertyName;
            ClassReferenceObject = classReferenceObject;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class OnFetchAddTo : Attribute
    {
        public string ClassReferenceObject;
        public OnFetchAddTo(string classReferenceObject)
        {
            ClassReferenceObject = classReferenceObject;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class GetParamAttribute : ParamAttribute
    {
        public GetParamAttribute(ParamType paramType, string param, Type type, string defaultValue = null, string internalMemberReference = null, string innerProperty = null) : base(paramType, param, type, defaultValue, internalMemberReference, innerProperty)
        {
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class UrlSegmentParamAttribute : ParamAttribute
    {
        public UrlSegmentParamAttribute(ParamType paramType, string param, Type type = null, string internalMemberReference = null, string innerProperty = null) : base(paramType, param, type, internalMemberReference: internalMemberReference, innerProperty: innerProperty)
        {
        }
    }


    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class CreateParamAttribute : ParamAttribute
    {
        public CreateParamAttribute(ParamType paramType, string param, Type type, string defaultValue = null, string internalMemberReference = null, string innerProperty = null) : base(paramType, param, type, defaultValue, internalMemberReference, innerProperty)
        {
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class AddToCollectionParamAttribute : ParamAttribute
    {
        public AddToCollectionParamAttribute(ParamType paramType, string param, Type type, string defaultValue = null, string internalMemberReference = null, string innerProperty = null) : base(paramType, param, type, defaultValue, internalMemberReference, innerProperty)
        {
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class AddToCollectionEndpointAttribute : Attribute
    {
        public AddToCollectionEndpointAttribute(string endpoint)
        {

        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class RemoveFromCollectionEndpointAttribute : Attribute
    {
        public RemoveFromCollectionEndpointAttribute(string endpoint)
        {

        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ApiTemplate : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ReadOnly : Attribute
    {
    }

    [Flags]
    public enum EndpointMethods
    {
        Get = 1 << 0,
        Put = 1 << 1,
        Post = 1 << 2,
        Delete = 1 << 3
    }

    public partial class Asana
    {
        [Endpoint("/users")]
        [ReadOnly]
        public List<AsanaUser> Users { get; set; }

        [Endpoint("/workspaces")]
        [ReadOnly]
        public List<AsanaWorkspace> Workspaces { get; set; }

        [Endpoint("/users/me")]
        [ReadOnly]
        public AsanaUser Me { get; set; }
    }

    [ApiTemplate]
    [Endpoint("/teams/{id}", EndpointMethods.Get)]
    [UrlSegmentParam(ParamType.Required, "id", type: typeof(Int64))]
    [ReadOnly]
    public partial class AsanaTeam
    {

    }

    [ApiTemplate]
    [Endpoint("/workspaces/{id}", EndpointMethods.Get)]
    [UrlSegmentParam(ParamType.Required, "id", type: typeof(Int64))]
    [ReadOnly]
    public partial class AsanaWorkspace
    {
        public string Name { get; set; }

        [Endpoint("/workspaces/{id}/projects", EndpointMethods.Get | EndpointMethods.Post)]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        [GetParam(ParamType.Optional, "archived", typeof(bool?))]
        [CreateParam(ParamType.Optional, "team", typeof(AsanaTeam))]
        [OnCreateSetForeignPropertyInside("Workspace", "this")]
        public List<AsanaProject> Projects { get; set; }

        [Endpoint("/workspaces/{id}/tasks")] //?assignee=me
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        [GetParam(ParamType.Internal, "assignee", typeof(AsanaUser), "me")]
        [GetParam(ParamType.Optional, "completed_since", typeof(DateTime?), "now")]
        [GetParam(ParamType.Optional, "modified_since", typeof(DateTime?))]
        public List<AsanaTask> MyTasks { get; set; }

        [Endpoint("/workspaces/{id}/tasks", EndpointMethods.Get | EndpointMethods.Post)]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        [GetParam(ParamType.Optional, "completed_since", typeof(DateTime?), "now")]
        [GetParam(ParamType.Optional, "modified_since", typeof(DateTime?))]
        [OnCreateSetForeignPropertyInside("Workspace", "this")]
        //        [GetParam(ParamType.Optional, "test", typeof(AsanaProject), innerProperty: "id")]
        public List<AsanaTask> Tasks { get; set; }
    }

    [ApiTemplate]
    [Endpoint("/projects/{id}", EndpointMethods.Get | EndpointMethods.Put | EndpointMethods.Delete)]
    [UrlSegmentParam(ParamType.Required, "id", type: typeof(Int64))]
    public partial class AsanaProject
    {
        public string Name { get; set; }
        public string Notes { get; set; }
    }

    [ApiTemplate]
    [Endpoint("/users/{id}", EndpointMethods.Get)]
    [UrlSegmentParam(ParamType.Required, "id", type: typeof(Int64))]
    public partial class AsanaUser
    {
        //        [Endpoint("/workspaces/{workspace.id}/tasks?assignee={id}")]
        [Endpoint("/tasks", EndpointMethods.Get | EndpointMethods.Post)]
        [GetParam(ParamType.Internal, "workspace", typeof(AsanaWorkspace), null, "workspace.id")]
        [GetParam(ParamType.Internal, "assignee", typeof(AsanaUser), null, "id")]
        [GetParam(ParamType.Optional, "completed_since", typeof(DateTime?), "now")]
        [GetParam(ParamType.Optional, "modified_since", typeof(DateTime?))]
        [OnCreateSetForeignPropertyInside("Workspace", "Workspace")]
        [OnCreateSetForeignPropertyInside("Assignee", "this")]
        [OnFetchAddTo("Workspace.Tasks")]
        [OnFetchAddTo("Asana.AllTasks")]
        public List<AsanaTask> Tasks { get; set; }

        public AsanaWorkspace Workspace { get; set; }

        public string Name { get; set; }
    }

    [ApiTemplate]
    [Endpoint("/tasks/{id}", EndpointMethods.Get | EndpointMethods.Put | EndpointMethods.Delete)]
    [UrlSegmentParam(ParamType.Required, "id", type: typeof(Int64))]
    public partial class AsanaTask
    {
        public bool? Completed { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<Dictionary<string, string>> Followers { get; set; }

        // TODO: automated [JsonProperty, JsonConverter(typeof(JsonRefedConverter))]
        public AsanaUser Assignee;
        public string AssigneeStatus { get; set; }

        [Endpoint("/tasks/{id}/projects")]
        [AddToCollectionEndpoint("/tasks/{id}/addProject")]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        [AddToCollectionParam(ParamType.Required, "project", typeof(AsanaProject))]
        [AddToCollectionParam(ParamType.Optional, "insert_after", typeof(AsanaTask))]
        [AddToCollectionParam(ParamType.Optional, "insert_before", typeof(AsanaTask))]
        //            [JsonProperty(ItemIsReference = true)]
        //            [JsonProperty(ItemConverterType = typeof(JsonRefConverter))]
        //            [JsonConverter(typeof(ProjectConverter))]
        // TODO: automated [JsonProperty(ItemConverterType = typeof(JsonRefedConverter))]
        public List<AsanaProject> Projects { get; set; }

        public string Name { get; set; }
        public string Notes { get; set; }

        /*
        {
            "data": {
            "assignee": {
                "id": 1235,
                "name": "Tim Bizarro"
            },
            "assignee_status": "inbox",
            "completed": false,
            "completed_at": null,
            "created_at": "2012-02-22T02:06:58.158Z",
            "due_on": null,
            "followers": [
                {
                "id": 5678,
                "name": "Greg Sanchez"
                }
            ],
            "id": 1001,
            "modified_at": "2012-02-22T02:06:58.158Z",
            "name": "Hello, world!",
            "notes": "How are you today?",
            "parent": null,
            "projects": [
                {
                "id": 14641,
                "name": "Cat Stuff"
                }
            ],
            "workspace": {
                "id": 14916,
                "name": "My Favorite Workspace"
            }
            }
        }
        */
    }

    public static class CodeGeneratorHelper
    {
        public static List<string> GetBracketedFieldsList(this string input)
        {
            var matches = Regex.Matches(input, @"{([^}]+?)}");
            if (matches.Count > 0)
                return (from Match match in matches
                        select match.Groups[1].Value).ToList();
            return null;
        }

        public static string MakeParametersString(this EndpointAttribute attribute)
        {
            if (String.IsNullOrEmpty(attribute.Endpoint) || attribute.ParameterTypes.Length == 0)
                return String.Empty;
            return attribute.ParameterTypes.MakeParametersString(attribute.Endpoint);
        }
        public static string MakeParametersString(this Type[] types, string inputString)
        {
            if (types == null)
            {
                return String.Empty;
            }
            var fields = inputString.GetBracketedFieldsList();
            var output = new List<string>();
            if (fields.Count >= types.Length)
            {
                for (int index = 0; index < types.Length; index++)
                {
                    var t = types[index];
                    output.Add(String.Format("{0} {1}", types[index].Name, fields[index]));
                }
            }
            return String.Join(", ", output);
        }
    }

    public static class TypeExtensions
    {
        public static string GetFriendlyName(this Type type)
        {

            // from http://stackoverflow.com/questions/4615553/c-sharp-get-user-friendly-name-of-simple-types-through-reflection
            string typeName;
            using (var provider = new CSharpCodeProvider())
            {
                var typeRef = new CodeTypeReference(type);
                typeName = provider.GetTypeOutput(typeRef);
            }
            return typeName
                    .Replace("System.Collections.Generic.", String.Empty)
                    .Replace("AsanaSharp.", String.Empty);
        }
    }

    public class UniversalProperty
    {
        private PropertyInfo _property;
        private Type _class;

        public Type Type
        {
            get
            {
                if (!ReferenceEquals(_property, null))
                {
                    return _property.PropertyType;
                }
                return _class;
            }
        }

        public bool IsReadOnly
        {
            get { return GetCustomAttributes<ReadOnly>(true).Any(); }
        }

        public bool IsList
        {
            get { return Type.IsList(); }
        }

        public bool IsProperty
        {
            get { return !ReferenceEquals(_property, null); }
        }

        public UniversalProperty(PropertyInfo propertyInfo)
        {
            _property = propertyInfo;
        }
        public UniversalProperty(Type cls)
        {
            _class = cls;
        }

        public string AsanaName
        {
            get
            {
                return Regex.Replace(
                    Name, @"([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", "$1$3_$2$4").ToLower();
            }
        }

        public string FriendlyTypeName
        {
            get
            {
                if (!ReferenceEquals(_property, null))
                {
                    return _property.PropertyType.GetFriendlyName();
                }
                else
                {
                    return _class.Name;
                }
            }
        }

        public string FriendlyTypeNameOrAsanaList
        {
            get
            {
                return IsReadOnly ? 
                    FriendlyTypeNameOrList(typeof(AsanaReadOnlyCollection<>)) :
                    FriendlyTypeNameOrList(typeof (AsanaCollection<>));
            }
        }

        public string FriendlyListName
        {
            get
            {
                return IsReadOnly ?
                    (typeof(AsanaReadOnlyCollection<>)).GetFriendlyName() :
                    (typeof(AsanaCollection<>)).GetFriendlyName();
            }
        }

        public string FriendlyInnerName
        {
            get
            {
                if (IsList)
                {
                    var innerType = Type.ListInnerType();
                    if (innerType != null)
                    {
                        return innerType.GetFriendlyName();
                    }
                }
                return null;
            }
        }

        public string FriendlyTypeNameOrList(Type listType)
        {
            return ReplaceListTypeName(Type, listType);
        }

        public static string ReplaceListTypeName(Type originalType, Type newListType)
        {
            var innerType = originalType.ListInnerType();
            if (innerType != null)
            {
                var innerName = innerType.GetFriendlyName();
                return String.Format("{0}<{1}>", newListType.GetFriendlyName().Replace("<>", String.Empty), innerName);
            }
            return originalType.GetFriendlyName();
        }

        public string Name
        {
            get
            {
                if (!ReferenceEquals(_property, null))
                {
                    return _property.Name;
                }
                else
                {
                    return _class.Name;
                }
            }
        }

        public IEnumerable<T> GetCustomAttributes<T>(bool inherit) where T : Attribute
        {
            if (!ReferenceEquals(_class, null))
                return _class.GetCustomAttributes<T>(inherit);
            return _property.GetCustomAttributes<T>(inherit);
        }

    }

    public class AsanaCollection<T> : ObservableCollection<T>
    {

    }

    public class AsanaReadOnlyCollection<T> : ObservableCollection<T>
    {

    }

    public static class TypeTools //where T : IList<T>
    {
        public static bool IsList(this Type type)
        {
            return type.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof (IList<>));
        }

        public static Type ListInnerType(this Type type)
        {
            if (type.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType &&
                                                          interfaceType.GetGenericTypeDefinition()
                                                          == typeof(IList<>)))
            {
                Type itemType = type.GetGenericArguments()[0];
                return itemType;
            }
            return null;
        }

        public static string Depluralize(this string pluralWord)
        {
            string retVal = pluralWord;
            if (pluralWord.EndsWith("ies"))
            {
                return retVal.TrimEnd('s').TrimEnd('e').TrimEnd('i') + 'y';
            }
            else if (pluralWord.EndsWith("s"))
            {
                return retVal.TrimEnd('s');
            }
            return retVal;
        }
        /*
        public static IEnumerable<Enum> GetFlags(Enum input)
        {
            return Enum.GetValues(input.GetType()).Cast<Enum>().Where(input.HasFlag);
        }
        */
    }

}
