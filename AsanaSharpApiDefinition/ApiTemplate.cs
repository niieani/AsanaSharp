using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanaSharp
{
    public class AsanaUserPhotos // dummy
    {
	}
	public class AsanaTaskMembership // dummy
	{
	}

	public partial class Asana
    {
        [Endpoint("/users", EndpointMethods.Get)]
        [ReadOnly]
        public List<AsanaUser> Users { get; set; }

        [Endpoint("/workspaces", EndpointMethods.Get)]
        [ReadOnly]
        public List<AsanaWorkspace> Workspaces { get; set; }

        [Endpoint("/users/me", EndpointMethods.Get)]
        [ReadOnly]
        public AsanaUser Me { get; set; }
    }

    [ApiTemplate]
    [Endpoint("/users/{id}", EndpointMethods.Get)]
    [UrlSegmentParam(ParamType.Required, "id", typeof(Int64))]
    public partial class AsanaUser
    {
        [ReadOnly]
        public string Name { get; set; }

        [ReadOnly]
        public string Email { get; set; }

        [ReadOnly]
        public AsanaUserPhotos Photo { get; set; }

        public List<AsanaWorkspace> Workspaces { get; set; }

        [Endpoint("/tasks", EndpointMethods.Get | EndpointMethods.Post)]
		[GetParam(ParamType.Required, "workspace", typeof(AsanaWorkspace))]
		[GetParam(ParamType.Internal, "assignee", typeof(AsanaUser), internalMemberReference: "id")]
        [GetParam(ParamType.Optional, "completed_since", typeof(DateTime?), "now")]
        [GetParam(ParamType.Optional, "modified_since", typeof(DateTime?))]
		[CreateParam(ParamType.Internal, "assignee", typeof(AsanaUser), internalMemberReference: "id")]
		[CreateParam(ParamType.Required, "workspace", typeof(AsanaWorkspace))]
		[CreateParam(ParamType.Optional, "memberships", typeof(AsanaTaskMembership[]))]
		[OnCreateSetForeignPropertyInside("Workspace", "Workspace")]
        [OnCreateSetForeignPropertyInside("Assignee", "this")]
        [OnFetchAddTo("Workspace.Tasks")]
        [OnFetchAddTo("Asana.AllTasks")]
        public List<AsanaTask> Tasks { get; set; }
    }


    [ApiTemplate]
    [Endpoint("/tasks/{id}", EndpointMethods.Get | EndpointMethods.Put | EndpointMethods.Delete)]
    [UrlSegmentParam(ParamType.Required, "id", type: typeof(Int64))]
    public partial class AsanaTask
    {
        public AsanaUser Assignee { get; set; }

        public string AssigneeStatus { get; set; }

        [ReadOnly]
        public DateTime? CreatedAt { get; set; }

        public bool? Completed { get; set; }

        [ReadOnly]
        public DateTime? CompletedAt { get; set; }

        public DateTime? DueOn { get; set; }

		[AddToCollectionEndpoint("/tasks/{id}/addFollowers")]
		[AddToCollectionParam(ParamType.Required, "followers", typeof(AsanaUser[]))]
		[RemoveFromCollectionEndpoint("/tasks/{id}/removeFollowers")]
		[RemoveFromCollectionParam(ParamType.Required, "followers", typeof(AsanaUser[]))]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        public List<AsanaUser> Followers { get; set; }

        public bool? Hearted { get; set; }

        [ReadOnly]
        public List<AsanaUser> Hearts { get; set; }

        [ReadOnly]
        public DateTime? ModifiedAt { get; set; }

        public string Name { get; set; }

		[ReadOnly]
        public string Notes { get; set; }

        public string HtmlNotes { get; set; }

        [ReadOnly]
        public int? NumHearts { get; set; }

//		[CreateOnly]
		[ReadOnly] // TODO: remove
		public List<AsanaTaskMembership> Memberships { get; set; }

        [Endpoint("/tasks/{id}/projects", EndpointMethods.Get)]
        [AddToCollectionEndpoint("/tasks/{id}/addProject")]
		[AddToCollectionParam(ParamType.Required, "project", typeof(AsanaProject))]
		[AddToCollectionParam(ParamType.Optional, "section", typeof(AsanaTask))]
		[AddToCollectionParam(ParamType.Optional, "insert_after", typeof(AsanaTask))]
        [AddToCollectionParam(ParamType.Optional, "insert_before", typeof(AsanaTask))]
        [RemoveFromCollectionEndpoint("/tasks/{id}/removeProject")]
        [RemoveFromCollectionParam(ParamType.Required, "project", typeof(AsanaProject))]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        public List<AsanaProject> Projects { get; set; }

        [Endpoint("/tasks/{id}/tags", EndpointMethods.Get)]
        [AddToCollectionEndpoint("/tasks/{id}/addTag")]
        [AddToCollectionParam(ParamType.Required, "tag", typeof(AsanaTag))]
        [RemoveFromCollectionEndpoint("/tasks/{id}/removeTag")]
        [RemoveFromCollectionParam(ParamType.Required, "tag", typeof(AsanaTag))]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        [ExcludeFromCreation]
        public List<AsanaTag> Tags { get; set; }

        // TODO: fix this
        [ReadOnly]
        [SetEndpoint("/tasks/{id}/setParent")]
		[SetParam(ParamType.Required, "parent", typeof(AsanaTask))]
		[UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
		//        [SetParam(ParamType.Required, "parent", typeof(AsanaTask))]
		public AsanaUser Parent { get; set; }

        [ReadOnly]
        public AsanaWorkspace Workspace { get; set; }

        [Endpoint("/tasks/{id}/subtasks", EndpointMethods.Get | EndpointMethods.Post)]
        [CreateParam(ParamType.Internal, "parent", typeof(AsanaTask), internalMemberReference: "id")]
//        [AddToCollectionEndpoint("/tasks/{id}/subtasks")]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
		[OnCreateSetForeignPropertyInside("Parent", "this")]
		[OnFetchAddTo("Workspace.Tasks")]
		[OnFetchAddTo("Asana.AllTasks")]
		[ReadOnly]
        public List<AsanaTask> Subtasks { get; set; }

        [Endpoint("/tasks/{id}/stories", EndpointMethods.Get)]
        [AddToCollectionEndpoint("/tasks/{id}/stories")]
        [AddToCollectionParam(ParamType.Required, "html_text", typeof(string))]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
		[ReadOnly]
		public List<AsanaStory> Stories { get; set; }


        [Endpoint("/tasks/{id}/attachments", EndpointMethods.Get | EndpointMethods.Upload)]
        [UploadParam(ParamType.Required, "file", typeof(string))]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
		[ReadOnly]
		public List<AsanaAttachment> Attachments { get; set; }
    }

    [ApiTemplate]
    [Endpoint("/projects/{id}", EndpointMethods.Get | EndpointMethods.Put | EndpointMethods.Delete)]
    [UrlSegmentParam(ParamType.Required, "id", typeof(Int64))]
    public partial class AsanaProject
    {
        public bool? Archived { get; set; }

        [ReadOnly]
        public DateTime? CreatedAt { get; set; }

		[AddToCollectionEndpoint("/projects/{id}/addFollowers")]
		[AddToCollectionParam(ParamType.Required, "followers", typeof(AsanaUser[]))]
		[RemoveFromCollectionEndpoint("/projects/{id}/removeFollowers")]
		[RemoveFromCollectionParam(ParamType.Required, "followers", typeof(AsanaUser[]))]
		[UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
		public List<AsanaUser> Followers { get; set; }

        [ReadOnly]
        public DateTime? ModifiedAt { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public string Notes { get; set; }

        [ReadOnly]
        public AsanaWorkspace Workspace { get; set; }

        [ReadOnly]
        public AsanaTeam Team { get; set; }

        [Endpoint("/projects/{id}/tasks", EndpointMethods.Get)]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
		[ReadOnly]
        public List<AsanaTask> Tasks { get; set; }
		
		[Endpoint("/projects/{id}/sections", EndpointMethods.Get)]
		[UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
		[ReadOnly]
		public List<AsanaTask> Sections { get; } 
    }

    [ApiTemplate]
    [Endpoint("/tags/{id}", EndpointMethods.Get | EndpointMethods.Put)]
    [UrlSegmentParam(ParamType.Required, "id", type: typeof(Int64))]
    public partial class AsanaTag
    {
        [ReadOnly]
        public DateTime? CreatedAt { get; set; }
		
		[AddToCollectionEndpoint("/tags/{id}/addFollowers")]
		[AddToCollectionParam(ParamType.Required, "followers", typeof(AsanaUser[]))]
		[RemoveFromCollectionEndpoint("/tags/{id}/removeFollowers")]
		[RemoveFromCollectionParam(ParamType.Required, "followers", typeof(AsanaUser[]))]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
		[ExcludeFromCreation]
        public List<AsanaUser> Followers { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public string Notes { get; set; }

        [ReadOnly]
        public AsanaWorkspace Workspace { get; set; }

        [ReadOnly]
        [Endpoint("/tags/{id}/tasks", EndpointMethods.Get)]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        public List<AsanaTask> Tasks { get; set; }
    }

    [ApiTemplate]
    [Endpoint("/stories/{id}", EndpointMethods.Get)]
    [UrlSegmentParam(ParamType.Required, "id", type: typeof(Int64))]
    public partial class AsanaStory
    {
        [ReadOnly]
        public DateTime? CreatedAt { get; set; }

        [ReadOnly]
        public AsanaUser CreatedBy { get; set; }

        public bool? Hearted { get; set; }

        [ReadOnly]
        public List<AsanaUser> Hearts { get; set; }

        [ReadOnly]
        public int? NumHearts { get; set; }

        [ReadOnly]
        public string Text { get; set; }

        [ReadOnly]
        public string HtmlText { get; set; }

        [ReadOnly]
        public AsanaTask Target { get; set; }

        [ReadOnly]
        public string Source { get; set; }

        [ReadOnly]
        public string Type { get; set; }
    }

    [ApiTemplate]
    [Endpoint("/workspaces/{id}", EndpointMethods.Get)]
    [UrlSegmentParam(ParamType.Required, "id", type: typeof(Int64))]
    [ReadOnly]
    public partial class AsanaWorkspace
    {
        [ReadOnly]
        public string Name { get; set; }

        [ReadOnly]
        public bool? IsOrganization { get; set; }

        [Endpoint("/workspaces/{id}/projects", EndpointMethods.Get | EndpointMethods.Post)]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        [GetParam(ParamType.Optional, "archived", typeof(bool?))]
        [CreateParam(ParamType.Optional, "team", typeof(AsanaTeam))]
        [OnCreateSetForeignPropertyInside("Workspace", "this")]
        public List<AsanaProject> Projects { get; set; }

        [Endpoint("/workspaces/{id}/tags", EndpointMethods.Get | EndpointMethods.Post)]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        [OnCreateSetForeignPropertyInside("Workspace", "this")]
        public List<AsanaTag> Tags { get; set; }

        [Endpoint("/workspaces/{id}/tasks", EndpointMethods.Get | EndpointMethods.Post)]
		[CreateParam(ParamType.Optional, "memberships", typeof(AsanaTaskMembership[]))]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        [GetParam(ParamType.Optional, "assignee", typeof(AsanaUser), "me")]
        [GetParam(ParamType.Optional, "completed_since", typeof(DateTime?), "now")]
        [GetParam(ParamType.Optional, "modified_since", typeof(DateTime?))]
        [OnCreateSetForeignPropertyInside("Workspace", "this")]
        public List<AsanaTask> Tasks { get; set; }

        [Endpoint("/organizations/{id}/teams", EndpointMethods.Get)]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        //[OnCreateSetForeignPropertyInside("Organization", "this")]
		[ReadOnly]
        public List<AsanaTeam> Teams { get; set; }

        [Endpoint("/workspaces/{id}/typeahead", EndpointMethods.Get)]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        [GetParam(ParamType.Internal, "type", typeof(string), "task")]
        [GetParam(ParamType.Required, "query", typeof(string))]
        [GetParam(ParamType.Optional, "limit", typeof(int?))]
        [OnCreateSetForeignPropertyInside("Workspace", "this")]
        public List<AsanaTask> QueryTasks { get; set; }

        [Endpoint("/workspaces/{id}/typeahead", EndpointMethods.Get)]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        [GetParam(ParamType.Internal, "type", typeof(string), "user")]
        [GetParam(ParamType.Required, "query", typeof(string))]
        [GetParam(ParamType.Optional, "limit", typeof(int?))]
        [OnCreateSetForeignPropertyInside("Workspace", "this")]
        public List<AsanaUser> QueryUsers { get; set; }

        [Endpoint("/workspaces/{id}/typeahead", EndpointMethods.Get)]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        [GetParam(ParamType.Internal, "type", typeof(string), "project")]
        [GetParam(ParamType.Required, "query", typeof(string))]
        [GetParam(ParamType.Optional, "limit", typeof(int?))]
        [OnCreateSetForeignPropertyInside("Workspace", "this")]
        public List<AsanaProject> QueryProjects { get; set; }

        [Endpoint("/workspaces/{id}/typeahead", EndpointMethods.Get)]
        [UrlSegmentParam(ParamType.Internal, "id", internalMemberReference: "id")]
        [GetParam(ParamType.Internal, "type", typeof(string), "tag")]
        [GetParam(ParamType.Required, "query", typeof(string))]
        [GetParam(ParamType.Optional, "limit", typeof(int?))]
        [OnCreateSetForeignPropertyInside("Workspace", "this")]
        public List<AsanaTag> QueryTags { get; set; }
    }


    [ApiTemplate]
    [Endpoint("/teams/{id}", EndpointMethods.Get)]
    [UrlSegmentParam(ParamType.Required, "id", type: typeof(Int64))]
    [ReadOnly]
    public partial class AsanaTeam
    {
        [ReadOnly]
        public string Name { get; set; }

        [ReadOnly]
        public AsanaWorkspace Organization { get; set; }
    }

    [ApiTemplate]
    [Endpoint("/attachments/{id}", EndpointMethods.Get)]
    [UrlSegmentParam(ParamType.Required, "id", type: typeof(Int64))]
    [ReadOnly]
    public partial class AsanaAttachment
    {
        [ReadOnly]
        public DateTime? CreatedAt { get; set; }

        [ReadOnly]
        public string DownloadUrl { get; set; }

        [ReadOnly]
        public string Host { get; set; }

        [ReadOnly]
        public string Name { get; set; }

        [ReadOnly]
        public AsanaTask Parent { get; set; }

        [ReadOnly]
        public string ViewUrl { get; set; }
    }
}
