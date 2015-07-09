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
using AsanaSharp;
using Microsoft.CSharp;

namespace AsanaSharp
{

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
        public string Name { get; set; }
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

}
