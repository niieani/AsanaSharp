using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AsanaSharp;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Runtime.Serialization;
using System.Linq;

namespace AsanaSharp
{
	/// <summary>
    /// Generated: Get by ID with the EndpointAttribute.
    /// </summary>
	//[JsonObject(MemberSerialization.OptIn)]
	public partial class Asana
	{
		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaReadOnlyCollection<AsanaUser> Users = new AsanaReadOnlyCollection<AsanaUser>();
        [JsonProperty(PropertyName = "users")]
		private List<AsanaUser> _deserializedUsers;

		/// <summary>
        /// Accesses the /users endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaReadOnlyCollection<AsanaUser>> GetUsers(string optFields = null)
		{
			var request = new RestRequest("/users", Method.GET);
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaUser>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Users, null))
				{
					Users = new AsanaReadOnlyCollection<AsanaUser>(resultsCollection);
				}
				else
				{
					Users.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaUserCollection.Import(resultsCollection, true);
				return Users;
			}
		}

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaReadOnlyCollection<AsanaWorkspace> Workspaces = new AsanaReadOnlyCollection<AsanaWorkspace>();
        [JsonProperty(PropertyName = "workspaces")]
		private List<AsanaWorkspace> _deserializedWorkspaces;

		/// <summary>
        /// Accesses the /workspaces endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaReadOnlyCollection<AsanaWorkspace>> GetWorkspaces(string optFields = null)
		{
			var request = new RestRequest("/workspaces", Method.GET);
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaWorkspace>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Workspaces, null))
				{
					Workspaces = new AsanaReadOnlyCollection<AsanaWorkspace>(resultsCollection);
				}
				else
				{
					Workspaces.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaWorkspaceCollection.Import(resultsCollection, true);
				return Workspaces;
			}
		}

		public AsanaUser Me
		{
			get 
			{
				return _remoteMe;
			}
        }
		
		[JsonProperty("me")]
        private AsanaUser _remoteMe
		{
			get 
			{
				return _remoteValueMe;
			}
			set
			{
				if (_remoteValueMe != value)
				{
					_remoteValueMe = value;
					OnPropertyChanged("Me");
				}
			}
		}
		private AsanaUser _remoteValueMe;


		/// <summary>
        /// Accesses the /users/me endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaUser> GetMe(string optFields = null)
		{
			var request = new RestRequest("/users/me", Method.GET);
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				return jsonData.Data.ToObject<AsanaUser>(AsanaHost.JsonDeserializer);
			}
		}


		/// <summary>
        /// Accesses the /users/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaUser> GetUser(long id, string optFields = null)
		{
			var request = new RestRequest("/users/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				return jsonData.Data.ToObject<AsanaUser>(AsanaHost.JsonDeserializer);
			}
		}


		/// <summary>
        /// Accesses the /tasks/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaTask> GetTask(long id, string optFields = null)
		{
			var request = new RestRequest("/tasks/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				return jsonData.Data.ToObject<AsanaTask>(AsanaHost.JsonDeserializer);
			}
		}


		/// <summary>
        /// Accesses the /tasks/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task DeleteTask(long id)
		{
			var request = new RestRequest("/tasks/{id}", Method.DELETE);
			request.AddUrlSegment("id", id.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}


		/// <summary>
        /// Accesses the /projects/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaProject> GetProject(long id, string optFields = null)
		{
			var request = new RestRequest("/projects/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				return jsonData.Data.ToObject<AsanaProject>(AsanaHost.JsonDeserializer);
			}
		}


		/// <summary>
        /// Accesses the /projects/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task DeleteProject(long id)
		{
			var request = new RestRequest("/projects/{id}", Method.DELETE);
			request.AddUrlSegment("id", id.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}


		/// <summary>
        /// Accesses the /tags/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaTag> GetTag(long id, string optFields = null)
		{
			var request = new RestRequest("/tags/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				return jsonData.Data.ToObject<AsanaTag>(AsanaHost.JsonDeserializer);
			}
		}


		/// <summary>
        /// Accesses the /stories/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaStory> GetStory(long id, string optFields = null)
		{
			var request = new RestRequest("/stories/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				return jsonData.Data.ToObject<AsanaStory>(AsanaHost.JsonDeserializer);
			}
		}


		/// <summary>
        /// Accesses the /workspaces/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaWorkspace> GetWorkspace(long id, string optFields = null)
		{
			var request = new RestRequest("/workspaces/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				return jsonData.Data.ToObject<AsanaWorkspace>(AsanaHost.JsonDeserializer);
			}
		}


		/// <summary>
        /// Accesses the /teams/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaTeam> GetTeam(long id, string optFields = null)
		{
			var request = new RestRequest("/teams/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				return jsonData.Data.ToObject<AsanaTeam>(AsanaHost.JsonDeserializer);
			}
		}


		/// <summary>
        /// Accesses the /attachments/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaAttachment> GetAttachment(long id, string optFields = null)
		{
			var request = new RestRequest("/attachments/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				return jsonData.Data.ToObject<AsanaAttachment>(AsanaHost.JsonDeserializer);
			}
		}

	} // END: public partial class Asana

	public partial class AsanaCache
	{
		internal AsanaReadOnlyCollection<AsanaUserPhotos> AsanaUserPhotosCollection = new AsanaReadOnlyCollection<AsanaUserPhotos>();
		internal AsanaReadOnlyCollection<AsanaTaskMembership> AsanaTaskMembershipCollection = new AsanaReadOnlyCollection<AsanaTaskMembership>();
		internal AsanaReadOnlyCollection<AsanaUser> AsanaUserCollection = new AsanaReadOnlyCollection<AsanaUser>();
		internal AsanaReadOnlyCollection<AsanaTask> AsanaTaskCollection = new AsanaReadOnlyCollection<AsanaTask>();
		internal AsanaReadOnlyCollection<AsanaProject> AsanaProjectCollection = new AsanaReadOnlyCollection<AsanaProject>();
		internal AsanaReadOnlyCollection<AsanaTag> AsanaTagCollection = new AsanaReadOnlyCollection<AsanaTag>();
		internal AsanaReadOnlyCollection<AsanaStory> AsanaStoryCollection = new AsanaReadOnlyCollection<AsanaStory>();
		internal AsanaReadOnlyCollection<AsanaWorkspace> AsanaWorkspaceCollection = new AsanaReadOnlyCollection<AsanaWorkspace>();
		internal AsanaReadOnlyCollection<AsanaTeam> AsanaTeamCollection = new AsanaReadOnlyCollection<AsanaTeam>();
		internal AsanaReadOnlyCollection<AsanaAttachment> AsanaAttachmentCollection = new AsanaReadOnlyCollection<AsanaAttachment>();
	}

	public partial class AsanaUser : AsanaResource
	{
		public string Name
		{
			get 
			{
				return _remoteName;
			}
        }
		
		[JsonProperty("name")]
        private string _remoteName
		{
			get 
			{
				return _remoteValueName;
			}
			set
			{
				if (_remoteValueName != value)
				{
					_remoteValueName = value;
					OnPropertyChanged("Name");
				}
			}
		}
		private string _remoteValueName;

		public string Email
		{
			get 
			{
				return _remoteEmail;
			}
        }
		
		[JsonProperty("email")]
        private string _remoteEmail
		{
			get 
			{
				return _remoteValueEmail;
			}
			set
			{
				if (_remoteValueEmail != value)
				{
					_remoteValueEmail = value;
					OnPropertyChanged("Email");
				}
			}
		}
		private string _remoteValueEmail;

		public AsanaUserPhotos Photo
		{
			get 
			{
				return _remotePhoto;
			}
        }
		
		[JsonProperty("photo")]
        private AsanaUserPhotos _remotePhoto
		{
			get 
			{
				return _remoteValuePhoto;
			}
			set
			{
				if (_remoteValuePhoto != value)
				{
					_remoteValuePhoto = value;
					OnPropertyChanged("Photo");
				}
			}
		}
		private AsanaUserPhotos _remoteValuePhoto;

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaWorkspace> Workspaces = new AsanaCollection<AsanaWorkspace>();
        [JsonProperty(PropertyName = "workspaces")]
		private List<AsanaWorkspace> _deserializedWorkspaces;
		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaTask> Tasks = new AsanaCollection<AsanaTask>();
        [JsonProperty(PropertyName = "tasks")]
		private List<AsanaTask> _deserializedTasks;

		/// <summary>
        /// Accesses the /tasks endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaTask>> GetTasks(AsanaWorkspace workspace, System.Nullable<System.DateTime> completedSince = null, System.Nullable<System.DateTime> modifiedSince = null, string optFields = null, bool completedSinceIsNow = false)
		{
			var request = new RestRequest("/tasks", Method.GET);
			request.AddParameter("workspace", workspace.ToString());
			request.AddParameter("assignee", Id.ToString());
			if (completedSinceIsNow)
				request.AddParameter("completed_since", "now");
			else
			if (!ReferenceEquals(completedSince, null))
				request.AddParameter("completed_since", completedSince.ToString());
			if (!ReferenceEquals(modifiedSince, null))
				request.AddParameter("modified_since", modifiedSince.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTask>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Tasks, null))
				{
					Tasks = new AsanaCollection<AsanaTask>(resultsCollection);
				}
				else
				{
					Tasks.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaTaskCollection.Import(resultsCollection, true);
				return Tasks;
			}
		}


		/// <summary>
        /// Accesses the /tasks endpoint.
        /// </summary>
        /// <returns></returns>
		public AsanaTask NewTask(AsanaWorkspace workspace)
		{
			var request = new RestRequest("/tasks", Method.POST);
			request.AddParameter("assignee", Id.ToString());
			request.AddParameter("workspace", workspace.ToString());
			return new AsanaTask {CreateRequest = request, AsanaHost = AsanaHost}; // TODO: random minus ID ?
		}

		// makes a dummy save action; TODO: remove this
		public async Task<AsanaUser> Save()
		{
			return this;
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			// Load Workspaces //
			if (!ReferenceEquals(_deserializedWorkspaces, null))
			{
				if (ReferenceEquals(Workspaces, null))
				{
					// first deserialization
					Workspaces = new AsanaCollection<AsanaWorkspace>(_deserializedWorkspaces);
					if (_deserializedWorkspaces.Any())
						OnPropertyChanged("Workspaces");
				}
				else
				{
					var anythingChanged = Workspaces.Import(_deserializedWorkspaces);
					if (anythingChanged)
						OnPropertyChanged("Workspaces");
				}

				// cleanup
				_deserializedWorkspaces = null;
			}
			else if (ReferenceEquals(Workspaces, null))
			{
				Workspaces = new AsanaCollection<AsanaWorkspace>();
			}
			// ----------------------------- //

			// Load Tasks //
			if (!ReferenceEquals(_deserializedTasks, null))
			{
				if (ReferenceEquals(Tasks, null))
				{
					// first deserialization
					Tasks = new AsanaCollection<AsanaTask>(_deserializedTasks);
					if (_deserializedTasks.Any())
						OnPropertyChanged("Tasks");
				}
				else
				{
					var anythingChanged = Tasks.Import(_deserializedTasks);
					if (anythingChanged)
						OnPropertyChanged("Tasks");
				}

				// cleanup
				_deserializedTasks = null;
			}
			else if (ReferenceEquals(Tasks, null))
			{
				Tasks = new AsanaCollection<AsanaTask>();
			}
			// ----------------------------- //

		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}

		public bool HasLocalChanges
		{
			get 
			{
				var hasChanges = false;
				return hasChanges;
			}
		}
	} // END class: AsanaUser

	public partial class AsanaTask : AsanaResource
	{
		public AsanaUser Assignee
		{
			get 
			{
				return _localAssignee ?? _remoteAssignee;
			}
			set
			{
				if (_remoteAssignee != value)
					_localAssignee = value;
			}
        }
		
		[JsonProperty("assignee")]
        private AsanaUser _remoteAssignee
		{
			get 
			{
				return _remoteValueAssignee;
			}
			set
			{
				if (_remoteValueAssignee != value)
				{
					_remoteValueAssignee = value;
					OnPropertyChanged("Assignee");
				}
			}
		}
		private AsanaUser _remoteValueAssignee;

		private AsanaUser _localAssignee;

		public string AssigneeStatus
		{
			get 
			{
				return _localAssigneeStatus ?? _remoteAssigneeStatus;
			}
			set
			{
				if (_remoteAssigneeStatus != value)
					_localAssigneeStatus = value;
			}
        }
		
		[JsonProperty("assignee_status")]
        private string _remoteAssigneeStatus
		{
			get 
			{
				return _remoteValueAssigneeStatus;
			}
			set
			{
				if (_remoteValueAssigneeStatus != value)
				{
					_remoteValueAssigneeStatus = value;
					OnPropertyChanged("AssigneeStatus");
				}
			}
		}
		private string _remoteValueAssigneeStatus;

		private string _localAssigneeStatus;

		public System.Nullable<System.DateTime> CreatedAt
		{
			get 
			{
				return _remoteCreatedAt;
			}
        }
		
		[JsonProperty("created_at")]
        private System.Nullable<System.DateTime> _remoteCreatedAt
		{
			get 
			{
				return _remoteValueCreatedAt;
			}
			set
			{
				if (_remoteValueCreatedAt != value)
				{
					_remoteValueCreatedAt = value;
					OnPropertyChanged("CreatedAt");
				}
			}
		}
		private System.Nullable<System.DateTime> _remoteValueCreatedAt;

		public System.Nullable<bool> Completed
		{
			get 
			{
				return _localCompleted ?? _remoteCompleted;
			}
			set
			{
				if (_remoteCompleted != value)
					_localCompleted = value;
			}
        }
		
		[JsonProperty("completed")]
        private System.Nullable<bool> _remoteCompleted
		{
			get 
			{
				return _remoteValueCompleted;
			}
			set
			{
				if (_remoteValueCompleted != value)
				{
					_remoteValueCompleted = value;
					OnPropertyChanged("Completed");
				}
			}
		}
		private System.Nullable<bool> _remoteValueCompleted;

		private System.Nullable<bool> _localCompleted;

		public System.Nullable<System.DateTime> CompletedAt
		{
			get 
			{
				return _remoteCompletedAt;
			}
        }
		
		[JsonProperty("completed_at")]
        private System.Nullable<System.DateTime> _remoteCompletedAt
		{
			get 
			{
				return _remoteValueCompletedAt;
			}
			set
			{
				if (_remoteValueCompletedAt != value)
				{
					_remoteValueCompletedAt = value;
					OnPropertyChanged("CompletedAt");
				}
			}
		}
		private System.Nullable<System.DateTime> _remoteValueCompletedAt;

		public System.Nullable<System.DateTime> DueOn
		{
			get 
			{
				return _localDueOn ?? _remoteDueOn;
			}
			set
			{
				if (_remoteDueOn != value)
					_localDueOn = value;
			}
        }
		
		[JsonProperty("due_on")]
        private System.Nullable<System.DateTime> _remoteDueOn
		{
			get 
			{
				return _remoteValueDueOn;
			}
			set
			{
				if (_remoteValueDueOn != value)
				{
					_remoteValueDueOn = value;
					OnPropertyChanged("DueOn");
				}
			}
		}
		private System.Nullable<System.DateTime> _remoteValueDueOn;

		private System.Nullable<System.DateTime> _localDueOn;

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaUser> Followers = new AsanaCollection<AsanaUser>();
        [JsonProperty(PropertyName = "followers")]
		private List<AsanaUser> _deserializedFollowers;

		/// <summary>
        /// Accesses the /tasks/{id}/addFollowers endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task AddFollower(AsanaUser[] followers)
		{
			var request = new RestRequest("/tasks/{id}/addFollowers", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("followers", followers.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}


		/// <summary>
        /// Accesses the /tasks/{id}/removeFollowers endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task RemoveFollower(AsanaUser[] followers)
		{
			var request = new RestRequest("/tasks/{id}/removeFollowers", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("followers", followers.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}

		public System.Nullable<bool> Hearted
		{
			get 
			{
				return _localHearted ?? _remoteHearted;
			}
			set
			{
				if (_remoteHearted != value)
					_localHearted = value;
			}
        }
		
		[JsonProperty("hearted")]
        private System.Nullable<bool> _remoteHearted
		{
			get 
			{
				return _remoteValueHearted;
			}
			set
			{
				if (_remoteValueHearted != value)
				{
					_remoteValueHearted = value;
					OnPropertyChanged("Hearted");
				}
			}
		}
		private System.Nullable<bool> _remoteValueHearted;

		private System.Nullable<bool> _localHearted;

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaReadOnlyCollection<AsanaUser> Hearts = new AsanaReadOnlyCollection<AsanaUser>();
        [JsonProperty(PropertyName = "hearts")]
		private List<AsanaUser> _deserializedHearts;
		public System.Nullable<System.DateTime> ModifiedAt
		{
			get 
			{
				return _remoteModifiedAt;
			}
        }
		
		[JsonProperty("modified_at")]
        private System.Nullable<System.DateTime> _remoteModifiedAt
		{
			get 
			{
				return _remoteValueModifiedAt;
			}
			set
			{
				if (_remoteValueModifiedAt != value)
				{
					_remoteValueModifiedAt = value;
					OnPropertyChanged("ModifiedAt");
				}
			}
		}
		private System.Nullable<System.DateTime> _remoteValueModifiedAt;

		public string Name
		{
			get 
			{
				return _localName ?? _remoteName;
			}
			set
			{
				if (_remoteName != value)
					_localName = value;
			}
        }
		
		[JsonProperty("name")]
        private string _remoteName
		{
			get 
			{
				return _remoteValueName;
			}
			set
			{
				if (_remoteValueName != value)
				{
					_remoteValueName = value;
					OnPropertyChanged("Name");
				}
			}
		}
		private string _remoteValueName;

		private string _localName;

		public string Notes
		{
			get 
			{
				return _remoteNotes;
			}
        }
		
		[JsonProperty("notes")]
        private string _remoteNotes
		{
			get 
			{
				return _remoteValueNotes;
			}
			set
			{
				if (_remoteValueNotes != value)
				{
					_remoteValueNotes = value;
					OnPropertyChanged("Notes");
				}
			}
		}
		private string _remoteValueNotes;

		public string HtmlNotes
		{
			get 
			{
				return _localHtmlNotes ?? _remoteHtmlNotes;
			}
			set
			{
				if (_remoteHtmlNotes != value)
					_localHtmlNotes = value;
			}
        }
		
		[JsonProperty("html_notes")]
        private string _remoteHtmlNotes
		{
			get 
			{
				return _remoteValueHtmlNotes;
			}
			set
			{
				if (_remoteValueHtmlNotes != value)
				{
					_remoteValueHtmlNotes = value;
					OnPropertyChanged("HtmlNotes");
				}
			}
		}
		private string _remoteValueHtmlNotes;

		private string _localHtmlNotes;

		public System.Nullable<int> NumHearts
		{
			get 
			{
				return _remoteNumHearts;
			}
        }
		
		[JsonProperty("num_hearts")]
        private System.Nullable<int> _remoteNumHearts
		{
			get 
			{
				return _remoteValueNumHearts;
			}
			set
			{
				if (_remoteValueNumHearts != value)
				{
					_remoteValueNumHearts = value;
					OnPropertyChanged("NumHearts");
				}
			}
		}
		private System.Nullable<int> _remoteValueNumHearts;

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaReadOnlyCollection<AsanaTaskMembership> Memberships = new AsanaReadOnlyCollection<AsanaTaskMembership>();
        [JsonProperty(PropertyName = "memberships")]
		private List<AsanaTaskMembership> _deserializedMemberships;
		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaProject> Projects = new AsanaCollection<AsanaProject>();
        [JsonProperty(PropertyName = "projects")]
		private List<AsanaProject> _deserializedProjects;

		/// <summary>
        /// Accesses the /tasks/{id}/projects endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaProject>> GetProjects(string optFields = null)
		{
			var request = new RestRequest("/tasks/{id}/projects", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaProject>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Projects, null))
				{
					Projects = new AsanaCollection<AsanaProject>(resultsCollection);
				}
				else
				{
					Projects.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaProjectCollection.Import(resultsCollection, true);
				return Projects;
			}
		}


		/// <summary>
        /// Accesses the /tasks/{id}/addProject endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task AddProject(AsanaProject project, AsanaTask section = null, AsanaTask insertAfter = null, AsanaTask insertBefore = null)
		{
			var request = new RestRequest("/tasks/{id}/addProject", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("project", project.ToString());
			if (!ReferenceEquals(section, null))
				request.AddParameter("section", section.ToString());
			if (!ReferenceEquals(insertAfter, null))
				request.AddParameter("insert_after", insertAfter.ToString());
			if (!ReferenceEquals(insertBefore, null))
				request.AddParameter("insert_before", insertBefore.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}


		/// <summary>
        /// Accesses the /tasks/{id}/removeProject endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task RemoveProject(AsanaProject project)
		{
			var request = new RestRequest("/tasks/{id}/removeProject", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("project", project.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaTag> Tags = new AsanaCollection<AsanaTag>();
        [JsonProperty(PropertyName = "tags")]
		private List<AsanaTag> _deserializedTags;

		/// <summary>
        /// Accesses the /tasks/{id}/tags endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaTag>> GetTags(string optFields = null)
		{
			var request = new RestRequest("/tasks/{id}/tags", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTag>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Tags, null))
				{
					Tags = new AsanaCollection<AsanaTag>(resultsCollection);
				}
				else
				{
					Tags.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaTagCollection.Import(resultsCollection, true);
				return Tags;
			}
		}


		/// <summary>
        /// Accesses the /tasks/{id}/addTag endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task AddTag(AsanaTag tag)
		{
			var request = new RestRequest("/tasks/{id}/addTag", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("tag", tag.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}


		/// <summary>
        /// Accesses the /tasks/{id}/removeTag endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task RemoveTag(AsanaTag tag)
		{
			var request = new RestRequest("/tasks/{id}/removeTag", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("tag", tag.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}

		public AsanaUser Parent
		{
			get 
			{
				return _remoteParent;
			}
        }
		
		[JsonProperty("parent")]
        private AsanaUser _remoteParent
		{
			get 
			{
				return _remoteValueParent;
			}
			set
			{
				if (_remoteValueParent != value)
				{
					_remoteValueParent = value;
					OnPropertyChanged("Parent");
				}
			}
		}
		private AsanaUser _remoteValueParent;


		/// <summary>
        /// Accesses the /tasks/{id}/setParent endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaUser> SetParent(AsanaTask parent)
		{
			var request = new RestRequest("/tasks/{id}/setParent", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("parent", parent.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				return jsonData.Data.ToObject<AsanaUser>(AsanaHost.JsonDeserializer);
			}
		}

		public AsanaWorkspace Workspace
		{
			get 
			{
				return _remoteWorkspace;
			}
        }
		
		[JsonProperty("workspace")]
        private AsanaWorkspace _remoteWorkspace
		{
			get 
			{
				return _remoteValueWorkspace;
			}
			set
			{
				if (_remoteValueWorkspace != value)
				{
					_remoteValueWorkspace = value;
					OnPropertyChanged("Workspace");
				}
			}
		}
		private AsanaWorkspace _remoteValueWorkspace;

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaReadOnlyCollection<AsanaTask> Subtasks = new AsanaReadOnlyCollection<AsanaTask>();
        [JsonProperty(PropertyName = "subtasks")]
		private List<AsanaTask> _deserializedSubtasks;

		/// <summary>
        /// Accesses the /tasks/{id}/subtasks endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaReadOnlyCollection<AsanaTask>> GetSubtasks(string optFields = null)
		{
			var request = new RestRequest("/tasks/{id}/subtasks", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTask>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Subtasks, null))
				{
					Subtasks = new AsanaReadOnlyCollection<AsanaTask>(resultsCollection);
				}
				else
				{
					Subtasks.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaTaskCollection.Import(resultsCollection, true);
				return Subtasks;
			}
		}


		/// <summary>
        /// Accesses the /tasks/{id}/subtasks endpoint.
        /// </summary>
        /// <returns></returns>
		public AsanaTask NewTask()
		{
			var request = new RestRequest("/tasks/{id}/subtasks", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("parent", Id.ToString());
			return new AsanaTask {CreateRequest = request, AsanaHost = AsanaHost}; // TODO: random minus ID ?
		}

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaReadOnlyCollection<AsanaStory> Stories = new AsanaReadOnlyCollection<AsanaStory>();
        [JsonProperty(PropertyName = "stories")]
		private List<AsanaStory> _deserializedStories;

		/// <summary>
        /// Accesses the /tasks/{id}/stories endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaReadOnlyCollection<AsanaStory>> GetStories(string optFields = null)
		{
			var request = new RestRequest("/tasks/{id}/stories", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaStory>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Stories, null))
				{
					Stories = new AsanaReadOnlyCollection<AsanaStory>(resultsCollection);
				}
				else
				{
					Stories.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaStoryCollection.Import(resultsCollection, true);
				return Stories;
			}
		}


		/// <summary>
        /// Accesses the /tasks/{id}/stories endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task AddStory(string htmlText)
		{
			var request = new RestRequest("/tasks/{id}/stories", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("html_text", htmlText.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaReadOnlyCollection<AsanaAttachment> Attachments = new AsanaReadOnlyCollection<AsanaAttachment>();
        [JsonProperty(PropertyName = "attachments")]
		private List<AsanaAttachment> _deserializedAttachments;

		/// <summary>
        /// Accesses the /tasks/{id}/attachments endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaReadOnlyCollection<AsanaAttachment>> GetAttachments(string optFields = null)
		{
			var request = new RestRequest("/tasks/{id}/attachments", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaAttachment>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Attachments, null))
				{
					Attachments = new AsanaReadOnlyCollection<AsanaAttachment>(resultsCollection);
				}
				else
				{
					Attachments.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaAttachmentCollection.Import(resultsCollection, true);
				return Attachments;
			}
		}

		internal RestRequest CreateRequest;
		public async Task<AsanaTask> Save()
		{
			if (HasLocalChanges)
			{
				RestRequest request;
				if (IsLocal && !ReferenceEquals(CreateRequest, null))
					request = CreateRequest;
				else request = new RestRequest("/tasks/{id}", Method.PUT);
            
				// TODO: iterate UrlSegmentParam
				request.AddUrlSegment("id", Id.ToString());

				// for each _local
				if (!ReferenceEquals(_localAssignee, null))
					request.AddParameter("assignee", _localAssignee.ToString());
				if (!ReferenceEquals(_localAssigneeStatus, null))
					request.AddParameter("assignee_status", _localAssigneeStatus.ToString());
				if (!ReferenceEquals(_localCompleted, null))
					request.AddParameter("completed", _localCompleted.ToString());
				if (!ReferenceEquals(_localDueOn, null))
					request.AddParameter("due_on", _localDueOn.ToString());

				if (IsLocal)
				{
					var count = 0;
					if (Followers.BufferCount > 0)
					{
						var FollowersBuffer = Followers.GetBufferAndPurge();
						foreach (var single in FollowersBuffer.AddBuffer)
						{
							// TODO: save only if possible
							await single.Save(); // save if not saved yet
							request.AddParameter("followers" + "[" + count + "]", single.Id.ToString());
							count++;
						}
					}
				}

				if (!ReferenceEquals(_localHearted, null))
					request.AddParameter("hearted", _localHearted.ToString());
				if (!ReferenceEquals(_localName, null))
					request.AddParameter("name", _localName.ToString());
				if (!ReferenceEquals(_localHtmlNotes, null))
					request.AddParameter("html_notes", _localHtmlNotes.ToString());

				if (IsLocal)
				{
					var count = 0;
					if (Projects.BufferCount > 0)
					{
						var ProjectsBuffer = Projects.GetBufferAndPurge();
						foreach (var single in ProjectsBuffer.AddBuffer)
						{
							// TODO: save only if possible
							await single.Save(); // save if not saved yet
							request.AddParameter("projects" + "[" + count + "]", single.Id.ToString());
							count++;
						}
					}
				}


				// execute the request
				var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
				// AsanaTask retVal;

				using (var stringReader = new StringReader(response.Content)) // raw content as string
				using (var reader = new JsonTextReader(stringReader))
				{
					var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
					if (!ReferenceEquals(jsonData.Errors, null)) // (!string.IsNullOrEmpty(jsonData.Errors.Message))
						throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);

				    _id = (Int64) jsonData.Data.Property("id");
                    AsanaHost.JsonCachingContext.SetReference<AsanaTask>(Id, this);
					//retVal = 
					jsonData.Data.ToObject<AsanaTask>(AsanaHost.JsonDeserializer);
				}
			}
			// nullify _local and update lists
			if (!ReferenceEquals(_localAssignee, null))
				_localAssignee = null;
			if (!ReferenceEquals(_localAssigneeStatus, null))
				_localAssigneeStatus = null;
			if (!ReferenceEquals(_localCompleted, null))
				_localCompleted = null;
			if (!ReferenceEquals(_localDueOn, null))
				_localDueOn = null;

			if (Followers.BufferCount > 0)
			{
				var FollowersBuffer = Followers.GetBufferAndPurge();
				await AddFollower(FollowersBuffer.AddBuffer.ToArray());
				await RemoveFollower(FollowersBuffer.RemoveBuffer.ToArray());
			}

			if (!ReferenceEquals(_localHearted, null))
				_localHearted = null;
			if (!ReferenceEquals(_localName, null))
				_localName = null;
			if (!ReferenceEquals(_localHtmlNotes, null))
				_localHtmlNotes = null;

			if (Projects.BufferCount > 0)
			{
				var ProjectsBuffer = Projects.GetBufferAndPurge();
				foreach (var single in ProjectsBuffer.AddBuffer)
				{
					// TODO: save only if possible
					await single.Save();
					await AddProject(single);
				}
				foreach (var single in ProjectsBuffer.RemoveBuffer)
				{
					await RemoveProject(single);
				}
			}


			if (Tags.BufferCount > 0)
			{
				var TagsBuffer = Tags.GetBufferAndPurge();
				foreach (var single in TagsBuffer.AddBuffer)
				{
					// TODO: save only if possible
					await single.Save();
					await AddTag(single);
				}
				foreach (var single in TagsBuffer.RemoveBuffer)
				{
					await RemoveTag(single);
				}
			}

				//return retVal;
			return this;
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			// Load Followers //
			if (!ReferenceEquals(_deserializedFollowers, null))
			{
				if (ReferenceEquals(Followers, null))
				{
					// first deserialization
					Followers = new AsanaCollection<AsanaUser>(_deserializedFollowers);
					if (_deserializedFollowers.Any())
						OnPropertyChanged("Followers");
				}
				else
				{
					var anythingChanged = Followers.Import(_deserializedFollowers);
					if (anythingChanged)
						OnPropertyChanged("Followers");
				}

				// cleanup
				_deserializedFollowers = null;
			}
			else if (ReferenceEquals(Followers, null))
			{
				Followers = new AsanaCollection<AsanaUser>();
			}
			// ----------------------------- //

			// Load Hearts //
			if (!ReferenceEquals(_deserializedHearts, null))
			{
				if (ReferenceEquals(Hearts, null))
				{
					// first deserialization
					Hearts = new AsanaReadOnlyCollection<AsanaUser>(_deserializedHearts);
					if (_deserializedHearts.Any())
						OnPropertyChanged("Hearts");
				}
				else
				{
					var anythingChanged = Hearts.Import(_deserializedHearts);
					if (anythingChanged)
						OnPropertyChanged("Hearts");
				}

				// cleanup
				_deserializedHearts = null;
			}
			else if (ReferenceEquals(Hearts, null))
			{
				Hearts = new AsanaReadOnlyCollection<AsanaUser>();
			}
			// ----------------------------- //

			// Load Memberships //
			if (!ReferenceEquals(_deserializedMemberships, null))
			{
				if (ReferenceEquals(Memberships, null))
				{
					// first deserialization
					Memberships = new AsanaReadOnlyCollection<AsanaTaskMembership>(_deserializedMemberships);
					if (_deserializedMemberships.Any())
						OnPropertyChanged("Memberships");
				}
				else
				{
					var anythingChanged = Memberships.Import(_deserializedMemberships);
					if (anythingChanged)
						OnPropertyChanged("Memberships");
				}

				// cleanup
				_deserializedMemberships = null;
			}
			else if (ReferenceEquals(Memberships, null))
			{
				Memberships = new AsanaReadOnlyCollection<AsanaTaskMembership>();
			}
			// ----------------------------- //

			// Load Projects //
			if (!ReferenceEquals(_deserializedProjects, null))
			{
				if (ReferenceEquals(Projects, null))
				{
					// first deserialization
					Projects = new AsanaCollection<AsanaProject>(_deserializedProjects);
					if (_deserializedProjects.Any())
						OnPropertyChanged("Projects");
				}
				else
				{
					var anythingChanged = Projects.Import(_deserializedProjects);
					if (anythingChanged)
						OnPropertyChanged("Projects");
				}

				// cleanup
				_deserializedProjects = null;
			}
			else if (ReferenceEquals(Projects, null))
			{
				Projects = new AsanaCollection<AsanaProject>();
			}
			// ----------------------------- //

			// Load Tags //
			if (!ReferenceEquals(_deserializedTags, null))
			{
				if (ReferenceEquals(Tags, null))
				{
					// first deserialization
					Tags = new AsanaCollection<AsanaTag>(_deserializedTags);
					if (_deserializedTags.Any())
						OnPropertyChanged("Tags");
				}
				else
				{
					var anythingChanged = Tags.Import(_deserializedTags);
					if (anythingChanged)
						OnPropertyChanged("Tags");
				}

				// cleanup
				_deserializedTags = null;
			}
			else if (ReferenceEquals(Tags, null))
			{
				Tags = new AsanaCollection<AsanaTag>();
			}
			// ----------------------------- //

			// Load Subtasks //
			if (!ReferenceEquals(_deserializedSubtasks, null))
			{
				if (ReferenceEquals(Subtasks, null))
				{
					// first deserialization
					Subtasks = new AsanaReadOnlyCollection<AsanaTask>(_deserializedSubtasks);
					if (_deserializedSubtasks.Any())
						OnPropertyChanged("Subtasks");
				}
				else
				{
					var anythingChanged = Subtasks.Import(_deserializedSubtasks);
					if (anythingChanged)
						OnPropertyChanged("Subtasks");
				}

				// cleanup
				_deserializedSubtasks = null;
			}
			else if (ReferenceEquals(Subtasks, null))
			{
				Subtasks = new AsanaReadOnlyCollection<AsanaTask>();
			}
			// ----------------------------- //

			// Load Stories //
			if (!ReferenceEquals(_deserializedStories, null))
			{
				if (ReferenceEquals(Stories, null))
				{
					// first deserialization
					Stories = new AsanaReadOnlyCollection<AsanaStory>(_deserializedStories);
					if (_deserializedStories.Any())
						OnPropertyChanged("Stories");
				}
				else
				{
					var anythingChanged = Stories.Import(_deserializedStories);
					if (anythingChanged)
						OnPropertyChanged("Stories");
				}

				// cleanup
				_deserializedStories = null;
			}
			else if (ReferenceEquals(Stories, null))
			{
				Stories = new AsanaReadOnlyCollection<AsanaStory>();
			}
			// ----------------------------- //

			// Load Attachments //
			if (!ReferenceEquals(_deserializedAttachments, null))
			{
				if (ReferenceEquals(Attachments, null))
				{
					// first deserialization
					Attachments = new AsanaReadOnlyCollection<AsanaAttachment>(_deserializedAttachments);
					if (_deserializedAttachments.Any())
						OnPropertyChanged("Attachments");
				}
				else
				{
					var anythingChanged = Attachments.Import(_deserializedAttachments);
					if (anythingChanged)
						OnPropertyChanged("Attachments");
				}

				// cleanup
				_deserializedAttachments = null;
			}
			else if (ReferenceEquals(Attachments, null))
			{
				Attachments = new AsanaReadOnlyCollection<AsanaAttachment>();
			}
			// ----------------------------- //

		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}

		public bool HasLocalChanges
		{
			get 
			{
				var hasChanges = false;
				if (!ReferenceEquals(_localAssignee, null))
					hasChanges = true;
				if (!ReferenceEquals(_localAssigneeStatus, null))
					hasChanges = true;
				if (!ReferenceEquals(_localCompleted, null))
					hasChanges = true;
				if (!ReferenceEquals(_localDueOn, null))
					hasChanges = true;
				if (!ReferenceEquals(_localHearted, null))
					hasChanges = true;
				if (!ReferenceEquals(_localName, null))
					hasChanges = true;
				if (!ReferenceEquals(_localHtmlNotes, null))
					hasChanges = true;
				return hasChanges;
			}
		}
	} // END class: AsanaTask

	public partial class AsanaProject : AsanaResource
	{
		public System.Nullable<bool> Archived
		{
			get 
			{
				return _localArchived ?? _remoteArchived;
			}
			set
			{
				if (_remoteArchived != value)
					_localArchived = value;
			}
        }
		
		[JsonProperty("archived")]
        private System.Nullable<bool> _remoteArchived
		{
			get 
			{
				return _remoteValueArchived;
			}
			set
			{
				if (_remoteValueArchived != value)
				{
					_remoteValueArchived = value;
					OnPropertyChanged("Archived");
				}
			}
		}
		private System.Nullable<bool> _remoteValueArchived;

		private System.Nullable<bool> _localArchived;

		public System.Nullable<System.DateTime> CreatedAt
		{
			get 
			{
				return _remoteCreatedAt;
			}
        }
		
		[JsonProperty("created_at")]
        private System.Nullable<System.DateTime> _remoteCreatedAt
		{
			get 
			{
				return _remoteValueCreatedAt;
			}
			set
			{
				if (_remoteValueCreatedAt != value)
				{
					_remoteValueCreatedAt = value;
					OnPropertyChanged("CreatedAt");
				}
			}
		}
		private System.Nullable<System.DateTime> _remoteValueCreatedAt;

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaUser> Followers = new AsanaCollection<AsanaUser>();
        [JsonProperty(PropertyName = "followers")]
		private List<AsanaUser> _deserializedFollowers;

		/// <summary>
        /// Accesses the /projects/{id}/addFollowers endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task AddFollower(AsanaUser[] followers)
		{
			var request = new RestRequest("/projects/{id}/addFollowers", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("followers", followers.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}


		/// <summary>
        /// Accesses the /projects/{id}/removeFollowers endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task RemoveFollower(AsanaUser[] followers)
		{
			var request = new RestRequest("/projects/{id}/removeFollowers", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("followers", followers.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}

		public System.Nullable<System.DateTime> ModifiedAt
		{
			get 
			{
				return _remoteModifiedAt;
			}
        }
		
		[JsonProperty("modified_at")]
        private System.Nullable<System.DateTime> _remoteModifiedAt
		{
			get 
			{
				return _remoteValueModifiedAt;
			}
			set
			{
				if (_remoteValueModifiedAt != value)
				{
					_remoteValueModifiedAt = value;
					OnPropertyChanged("ModifiedAt");
				}
			}
		}
		private System.Nullable<System.DateTime> _remoteValueModifiedAt;

		public string Name
		{
			get 
			{
				return _localName ?? _remoteName;
			}
			set
			{
				if (_remoteName != value)
					_localName = value;
			}
        }
		
		[JsonProperty("name")]
        private string _remoteName
		{
			get 
			{
				return _remoteValueName;
			}
			set
			{
				if (_remoteValueName != value)
				{
					_remoteValueName = value;
					OnPropertyChanged("Name");
				}
			}
		}
		private string _remoteValueName;

		private string _localName;

		public string Color
		{
			get 
			{
				return _localColor ?? _remoteColor;
			}
			set
			{
				if (_remoteColor != value)
					_localColor = value;
			}
        }
		
		[JsonProperty("color")]
        private string _remoteColor
		{
			get 
			{
				return _remoteValueColor;
			}
			set
			{
				if (_remoteValueColor != value)
				{
					_remoteValueColor = value;
					OnPropertyChanged("Color");
				}
			}
		}
		private string _remoteValueColor;

		private string _localColor;

		public string Notes
		{
			get 
			{
				return _localNotes ?? _remoteNotes;
			}
			set
			{
				if (_remoteNotes != value)
					_localNotes = value;
			}
        }
		
		[JsonProperty("notes")]
        private string _remoteNotes
		{
			get 
			{
				return _remoteValueNotes;
			}
			set
			{
				if (_remoteValueNotes != value)
				{
					_remoteValueNotes = value;
					OnPropertyChanged("Notes");
				}
			}
		}
		private string _remoteValueNotes;

		private string _localNotes;

		public AsanaWorkspace Workspace
		{
			get 
			{
				return _remoteWorkspace;
			}
        }
		
		[JsonProperty("workspace")]
        private AsanaWorkspace _remoteWorkspace
		{
			get 
			{
				return _remoteValueWorkspace;
			}
			set
			{
				if (_remoteValueWorkspace != value)
				{
					_remoteValueWorkspace = value;
					OnPropertyChanged("Workspace");
				}
			}
		}
		private AsanaWorkspace _remoteValueWorkspace;

		public AsanaTeam Team
		{
			get 
			{
				return _remoteTeam;
			}
        }
		
		[JsonProperty("team")]
        private AsanaTeam _remoteTeam
		{
			get 
			{
				return _remoteValueTeam;
			}
			set
			{
				if (_remoteValueTeam != value)
				{
					_remoteValueTeam = value;
					OnPropertyChanged("Team");
				}
			}
		}
		private AsanaTeam _remoteValueTeam;

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaReadOnlyCollection<AsanaTask> Tasks = new AsanaReadOnlyCollection<AsanaTask>();
        [JsonProperty(PropertyName = "tasks")]
		private List<AsanaTask> _deserializedTasks;

		/// <summary>
        /// Accesses the /projects/{id}/tasks endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaReadOnlyCollection<AsanaTask>> GetTasks(string optFields = null)
		{
			var request = new RestRequest("/projects/{id}/tasks", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTask>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Tasks, null))
				{
					Tasks = new AsanaReadOnlyCollection<AsanaTask>(resultsCollection);
				}
				else
				{
					Tasks.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaTaskCollection.Import(resultsCollection, true);
				return Tasks;
			}
		}

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaReadOnlyCollection<AsanaTask> Sections = new AsanaReadOnlyCollection<AsanaTask>();
        [JsonProperty(PropertyName = "sections")]
		private List<AsanaTask> _deserializedSections;

		/// <summary>
        /// Accesses the /projects/{id}/sections endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaReadOnlyCollection<AsanaTask>> GetSections(string optFields = null)
		{
			var request = new RestRequest("/projects/{id}/sections", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTask>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Sections, null))
				{
					Sections = new AsanaReadOnlyCollection<AsanaTask>(resultsCollection);
				}
				else
				{
					Sections.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaTaskCollection.Import(resultsCollection, true);
				return Sections;
			}
		}

		internal RestRequest CreateRequest;
		public async Task<AsanaProject> Save()
		{
			if (HasLocalChanges)
			{
				RestRequest request;
				if (IsLocal && !ReferenceEquals(CreateRequest, null))
					request = CreateRequest;
				else request = new RestRequest("/projects/{id}", Method.PUT);
            
				// TODO: iterate UrlSegmentParam
				request.AddUrlSegment("id", Id.ToString());

				// for each _local
				if (!ReferenceEquals(_localArchived, null))
					request.AddParameter("archived", _localArchived.ToString());

				if (IsLocal)
				{
					var count = 0;
					if (Followers.BufferCount > 0)
					{
						var FollowersBuffer = Followers.GetBufferAndPurge();
						foreach (var single in FollowersBuffer.AddBuffer)
						{
							// TODO: save only if possible
							await single.Save(); // save if not saved yet
							request.AddParameter("followers" + "[" + count + "]", single.Id.ToString());
							count++;
						}
					}
				}

				if (!ReferenceEquals(_localName, null))
					request.AddParameter("name", _localName.ToString());
				if (!ReferenceEquals(_localColor, null))
					request.AddParameter("color", _localColor.ToString());
				if (!ReferenceEquals(_localNotes, null))
					request.AddParameter("notes", _localNotes.ToString());

				// execute the request
				var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
				// AsanaProject retVal;

				using (var stringReader = new StringReader(response.Content)) // raw content as string
				using (var reader = new JsonTextReader(stringReader))
				{
					var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
					if (!ReferenceEquals(jsonData.Errors, null)) // (!string.IsNullOrEmpty(jsonData.Errors.Message))
						throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);

				    _id = (Int64) jsonData.Data.Property("id");
                    AsanaHost.JsonCachingContext.SetReference<AsanaProject>(Id, this);
					//retVal = 
					jsonData.Data.ToObject<AsanaProject>(AsanaHost.JsonDeserializer);
				}
			}
			// nullify _local and update lists
			if (!ReferenceEquals(_localArchived, null))
				_localArchived = null;

			if (Followers.BufferCount > 0)
			{
				var FollowersBuffer = Followers.GetBufferAndPurge();
				await AddFollower(FollowersBuffer.AddBuffer.ToArray());
				await RemoveFollower(FollowersBuffer.RemoveBuffer.ToArray());
			}

			if (!ReferenceEquals(_localName, null))
				_localName = null;
			if (!ReferenceEquals(_localColor, null))
				_localColor = null;
			if (!ReferenceEquals(_localNotes, null))
				_localNotes = null;
				//return retVal;
			return this;
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			// Load Followers //
			if (!ReferenceEquals(_deserializedFollowers, null))
			{
				if (ReferenceEquals(Followers, null))
				{
					// first deserialization
					Followers = new AsanaCollection<AsanaUser>(_deserializedFollowers);
					if (_deserializedFollowers.Any())
						OnPropertyChanged("Followers");
				}
				else
				{
					var anythingChanged = Followers.Import(_deserializedFollowers);
					if (anythingChanged)
						OnPropertyChanged("Followers");
				}

				// cleanup
				_deserializedFollowers = null;
			}
			else if (ReferenceEquals(Followers, null))
			{
				Followers = new AsanaCollection<AsanaUser>();
			}
			// ----------------------------- //

			// Load Tasks //
			if (!ReferenceEquals(_deserializedTasks, null))
			{
				if (ReferenceEquals(Tasks, null))
				{
					// first deserialization
					Tasks = new AsanaReadOnlyCollection<AsanaTask>(_deserializedTasks);
					if (_deserializedTasks.Any())
						OnPropertyChanged("Tasks");
				}
				else
				{
					var anythingChanged = Tasks.Import(_deserializedTasks);
					if (anythingChanged)
						OnPropertyChanged("Tasks");
				}

				// cleanup
				_deserializedTasks = null;
			}
			else if (ReferenceEquals(Tasks, null))
			{
				Tasks = new AsanaReadOnlyCollection<AsanaTask>();
			}
			// ----------------------------- //

			// Load Sections //
			if (!ReferenceEquals(_deserializedSections, null))
			{
				if (ReferenceEquals(Sections, null))
				{
					// first deserialization
					Sections = new AsanaReadOnlyCollection<AsanaTask>(_deserializedSections);
					if (_deserializedSections.Any())
						OnPropertyChanged("Sections");
				}
				else
				{
					var anythingChanged = Sections.Import(_deserializedSections);
					if (anythingChanged)
						OnPropertyChanged("Sections");
				}

				// cleanup
				_deserializedSections = null;
			}
			else if (ReferenceEquals(Sections, null))
			{
				Sections = new AsanaReadOnlyCollection<AsanaTask>();
			}
			// ----------------------------- //

		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}

		public bool HasLocalChanges
		{
			get 
			{
				var hasChanges = false;
				if (!ReferenceEquals(_localArchived, null))
					hasChanges = true;
				if (!ReferenceEquals(_localName, null))
					hasChanges = true;
				if (!ReferenceEquals(_localColor, null))
					hasChanges = true;
				if (!ReferenceEquals(_localNotes, null))
					hasChanges = true;
				return hasChanges;
			}
		}
	} // END class: AsanaProject

	public partial class AsanaTag : AsanaResource
	{
		public System.Nullable<System.DateTime> CreatedAt
		{
			get 
			{
				return _remoteCreatedAt;
			}
        }
		
		[JsonProperty("created_at")]
        private System.Nullable<System.DateTime> _remoteCreatedAt
		{
			get 
			{
				return _remoteValueCreatedAt;
			}
			set
			{
				if (_remoteValueCreatedAt != value)
				{
					_remoteValueCreatedAt = value;
					OnPropertyChanged("CreatedAt");
				}
			}
		}
		private System.Nullable<System.DateTime> _remoteValueCreatedAt;

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaUser> Followers = new AsanaCollection<AsanaUser>();
        [JsonProperty(PropertyName = "followers")]
		private List<AsanaUser> _deserializedFollowers;

		/// <summary>
        /// Accesses the /tags/{id}/addFollowers endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task AddFollower(AsanaUser[] followers)
		{
			var request = new RestRequest("/tags/{id}/addFollowers", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("followers", followers.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}


		/// <summary>
        /// Accesses the /tags/{id}/removeFollowers endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task RemoveFollower(AsanaUser[] followers)
		{
			var request = new RestRequest("/tags/{id}/removeFollowers", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("followers", followers.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
			}
		}

		public string Name
		{
			get 
			{
				return _localName ?? _remoteName;
			}
			set
			{
				if (_remoteName != value)
					_localName = value;
			}
        }
		
		[JsonProperty("name")]
        private string _remoteName
		{
			get 
			{
				return _remoteValueName;
			}
			set
			{
				if (_remoteValueName != value)
				{
					_remoteValueName = value;
					OnPropertyChanged("Name");
				}
			}
		}
		private string _remoteValueName;

		private string _localName;

		public string Color
		{
			get 
			{
				return _localColor ?? _remoteColor;
			}
			set
			{
				if (_remoteColor != value)
					_localColor = value;
			}
        }
		
		[JsonProperty("color")]
        private string _remoteColor
		{
			get 
			{
				return _remoteValueColor;
			}
			set
			{
				if (_remoteValueColor != value)
				{
					_remoteValueColor = value;
					OnPropertyChanged("Color");
				}
			}
		}
		private string _remoteValueColor;

		private string _localColor;

		public string Notes
		{
			get 
			{
				return _localNotes ?? _remoteNotes;
			}
			set
			{
				if (_remoteNotes != value)
					_localNotes = value;
			}
        }
		
		[JsonProperty("notes")]
        private string _remoteNotes
		{
			get 
			{
				return _remoteValueNotes;
			}
			set
			{
				if (_remoteValueNotes != value)
				{
					_remoteValueNotes = value;
					OnPropertyChanged("Notes");
				}
			}
		}
		private string _remoteValueNotes;

		private string _localNotes;

		public AsanaWorkspace Workspace
		{
			get 
			{
				return _remoteWorkspace;
			}
        }
		
		[JsonProperty("workspace")]
        private AsanaWorkspace _remoteWorkspace
		{
			get 
			{
				return _remoteValueWorkspace;
			}
			set
			{
				if (_remoteValueWorkspace != value)
				{
					_remoteValueWorkspace = value;
					OnPropertyChanged("Workspace");
				}
			}
		}
		private AsanaWorkspace _remoteValueWorkspace;

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaReadOnlyCollection<AsanaTask> Tasks = new AsanaReadOnlyCollection<AsanaTask>();
        [JsonProperty(PropertyName = "tasks")]
		private List<AsanaTask> _deserializedTasks;

		/// <summary>
        /// Accesses the /tags/{id}/tasks endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaReadOnlyCollection<AsanaTask>> GetTasks(string optFields = null)
		{
			var request = new RestRequest("/tags/{id}/tasks", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTask>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Tasks, null))
				{
					Tasks = new AsanaReadOnlyCollection<AsanaTask>(resultsCollection);
				}
				else
				{
					Tasks.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaTaskCollection.Import(resultsCollection, true);
				return Tasks;
			}
		}

		internal RestRequest CreateRequest;
		public async Task<AsanaTag> Save()
		{
			if (HasLocalChanges)
			{
				RestRequest request;
				if (IsLocal && !ReferenceEquals(CreateRequest, null))
					request = CreateRequest;
				else request = new RestRequest("/tags/{id}", Method.PUT);
            
				// TODO: iterate UrlSegmentParam
				request.AddUrlSegment("id", Id.ToString());

				// for each _local
				if (!ReferenceEquals(_localName, null))
					request.AddParameter("name", _localName.ToString());
				if (!ReferenceEquals(_localColor, null))
					request.AddParameter("color", _localColor.ToString());
				if (!ReferenceEquals(_localNotes, null))
					request.AddParameter("notes", _localNotes.ToString());

				// execute the request
				var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
				// AsanaTag retVal;

				using (var stringReader = new StringReader(response.Content)) // raw content as string
				using (var reader = new JsonTextReader(stringReader))
				{
					var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
					if (!ReferenceEquals(jsonData.Errors, null)) // (!string.IsNullOrEmpty(jsonData.Errors.Message))
						throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);

				    _id = (Int64) jsonData.Data.Property("id");
                    AsanaHost.JsonCachingContext.SetReference<AsanaTag>(Id, this);
					//retVal = 
					jsonData.Data.ToObject<AsanaTag>(AsanaHost.JsonDeserializer);
				}
			}
			// nullify _local and update lists

			if (Followers.BufferCount > 0)
			{
				var FollowersBuffer = Followers.GetBufferAndPurge();
				await AddFollower(FollowersBuffer.AddBuffer.ToArray());
				await RemoveFollower(FollowersBuffer.RemoveBuffer.ToArray());
			}

			if (!ReferenceEquals(_localName, null))
				_localName = null;
			if (!ReferenceEquals(_localColor, null))
				_localColor = null;
			if (!ReferenceEquals(_localNotes, null))
				_localNotes = null;
				//return retVal;
			return this;
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			// Load Followers //
			if (!ReferenceEquals(_deserializedFollowers, null))
			{
				if (ReferenceEquals(Followers, null))
				{
					// first deserialization
					Followers = new AsanaCollection<AsanaUser>(_deserializedFollowers);
					if (_deserializedFollowers.Any())
						OnPropertyChanged("Followers");
				}
				else
				{
					var anythingChanged = Followers.Import(_deserializedFollowers);
					if (anythingChanged)
						OnPropertyChanged("Followers");
				}

				// cleanup
				_deserializedFollowers = null;
			}
			else if (ReferenceEquals(Followers, null))
			{
				Followers = new AsanaCollection<AsanaUser>();
			}
			// ----------------------------- //

			// Load Tasks //
			if (!ReferenceEquals(_deserializedTasks, null))
			{
				if (ReferenceEquals(Tasks, null))
				{
					// first deserialization
					Tasks = new AsanaReadOnlyCollection<AsanaTask>(_deserializedTasks);
					if (_deserializedTasks.Any())
						OnPropertyChanged("Tasks");
				}
				else
				{
					var anythingChanged = Tasks.Import(_deserializedTasks);
					if (anythingChanged)
						OnPropertyChanged("Tasks");
				}

				// cleanup
				_deserializedTasks = null;
			}
			else if (ReferenceEquals(Tasks, null))
			{
				Tasks = new AsanaReadOnlyCollection<AsanaTask>();
			}
			// ----------------------------- //

		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}

		public bool HasLocalChanges
		{
			get 
			{
				var hasChanges = false;
				if (!ReferenceEquals(_localName, null))
					hasChanges = true;
				if (!ReferenceEquals(_localColor, null))
					hasChanges = true;
				if (!ReferenceEquals(_localNotes, null))
					hasChanges = true;
				return hasChanges;
			}
		}
	} // END class: AsanaTag

	public partial class AsanaStory : AsanaResource
	{
		public System.Nullable<System.DateTime> CreatedAt
		{
			get 
			{
				return _remoteCreatedAt;
			}
        }
		
		[JsonProperty("created_at")]
        private System.Nullable<System.DateTime> _remoteCreatedAt
		{
			get 
			{
				return _remoteValueCreatedAt;
			}
			set
			{
				if (_remoteValueCreatedAt != value)
				{
					_remoteValueCreatedAt = value;
					OnPropertyChanged("CreatedAt");
				}
			}
		}
		private System.Nullable<System.DateTime> _remoteValueCreatedAt;

		public AsanaUser CreatedBy
		{
			get 
			{
				return _remoteCreatedBy;
			}
        }
		
		[JsonProperty("created_by")]
        private AsanaUser _remoteCreatedBy
		{
			get 
			{
				return _remoteValueCreatedBy;
			}
			set
			{
				if (_remoteValueCreatedBy != value)
				{
					_remoteValueCreatedBy = value;
					OnPropertyChanged("CreatedBy");
				}
			}
		}
		private AsanaUser _remoteValueCreatedBy;

		public System.Nullable<bool> Hearted
		{
			get 
			{
				return _localHearted ?? _remoteHearted;
			}
			set
			{
				if (_remoteHearted != value)
					_localHearted = value;
			}
        }
		
		[JsonProperty("hearted")]
        private System.Nullable<bool> _remoteHearted
		{
			get 
			{
				return _remoteValueHearted;
			}
			set
			{
				if (_remoteValueHearted != value)
				{
					_remoteValueHearted = value;
					OnPropertyChanged("Hearted");
				}
			}
		}
		private System.Nullable<bool> _remoteValueHearted;

		private System.Nullable<bool> _localHearted;

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaReadOnlyCollection<AsanaUser> Hearts = new AsanaReadOnlyCollection<AsanaUser>();
        [JsonProperty(PropertyName = "hearts")]
		private List<AsanaUser> _deserializedHearts;
		public System.Nullable<int> NumHearts
		{
			get 
			{
				return _remoteNumHearts;
			}
        }
		
		[JsonProperty("num_hearts")]
        private System.Nullable<int> _remoteNumHearts
		{
			get 
			{
				return _remoteValueNumHearts;
			}
			set
			{
				if (_remoteValueNumHearts != value)
				{
					_remoteValueNumHearts = value;
					OnPropertyChanged("NumHearts");
				}
			}
		}
		private System.Nullable<int> _remoteValueNumHearts;

		public string Text
		{
			get 
			{
				return _remoteText;
			}
        }
		
		[JsonProperty("text")]
        private string _remoteText
		{
			get 
			{
				return _remoteValueText;
			}
			set
			{
				if (_remoteValueText != value)
				{
					_remoteValueText = value;
					OnPropertyChanged("Text");
				}
			}
		}
		private string _remoteValueText;

		public string HtmlText
		{
			get 
			{
				return _remoteHtmlText;
			}
        }
		
		[JsonProperty("html_text")]
        private string _remoteHtmlText
		{
			get 
			{
				return _remoteValueHtmlText;
			}
			set
			{
				if (_remoteValueHtmlText != value)
				{
					_remoteValueHtmlText = value;
					OnPropertyChanged("HtmlText");
				}
			}
		}
		private string _remoteValueHtmlText;

		public AsanaTask Target
		{
			get 
			{
				return _remoteTarget;
			}
        }
		
		[JsonProperty("target")]
        private AsanaTask _remoteTarget
		{
			get 
			{
				return _remoteValueTarget;
			}
			set
			{
				if (_remoteValueTarget != value)
				{
					_remoteValueTarget = value;
					OnPropertyChanged("Target");
				}
			}
		}
		private AsanaTask _remoteValueTarget;

		public string Source
		{
			get 
			{
				return _remoteSource;
			}
        }
		
		[JsonProperty("source")]
        private string _remoteSource
		{
			get 
			{
				return _remoteValueSource;
			}
			set
			{
				if (_remoteValueSource != value)
				{
					_remoteValueSource = value;
					OnPropertyChanged("Source");
				}
			}
		}
		private string _remoteValueSource;

		public string Type
		{
			get 
			{
				return _remoteType;
			}
        }
		
		[JsonProperty("type")]
        private string _remoteType
		{
			get 
			{
				return _remoteValueType;
			}
			set
			{
				if (_remoteValueType != value)
				{
					_remoteValueType = value;
					OnPropertyChanged("Type");
				}
			}
		}
		private string _remoteValueType;

		// makes a dummy save action; TODO: remove this
		public async Task<AsanaStory> Save()
		{
			return this;
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			// Load Hearts //
			if (!ReferenceEquals(_deserializedHearts, null))
			{
				if (ReferenceEquals(Hearts, null))
				{
					// first deserialization
					Hearts = new AsanaReadOnlyCollection<AsanaUser>(_deserializedHearts);
					if (_deserializedHearts.Any())
						OnPropertyChanged("Hearts");
				}
				else
				{
					var anythingChanged = Hearts.Import(_deserializedHearts);
					if (anythingChanged)
						OnPropertyChanged("Hearts");
				}

				// cleanup
				_deserializedHearts = null;
			}
			else if (ReferenceEquals(Hearts, null))
			{
				Hearts = new AsanaReadOnlyCollection<AsanaUser>();
			}
			// ----------------------------- //

		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}

		public bool HasLocalChanges
		{
			get 
			{
				var hasChanges = false;
				if (!ReferenceEquals(_localHearted, null))
					hasChanges = true;
				return hasChanges;
			}
		}
	} // END class: AsanaStory

	public partial class AsanaWorkspace : AsanaResource
	{
		public string Name
		{
			get 
			{
				return _remoteName;
			}
        }
		
		[JsonProperty("name")]
        private string _remoteName
		{
			get 
			{
				return _remoteValueName;
			}
			set
			{
				if (_remoteValueName != value)
				{
					_remoteValueName = value;
					OnPropertyChanged("Name");
				}
			}
		}
		private string _remoteValueName;

		public System.Nullable<bool> IsOrganization
		{
			get 
			{
				return _remoteIsOrganization;
			}
        }
		
		[JsonProperty("is_organization")]
        private System.Nullable<bool> _remoteIsOrganization
		{
			get 
			{
				return _remoteValueIsOrganization;
			}
			set
			{
				if (_remoteValueIsOrganization != value)
				{
					_remoteValueIsOrganization = value;
					OnPropertyChanged("IsOrganization");
				}
			}
		}
		private System.Nullable<bool> _remoteValueIsOrganization;

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaProject> Projects = new AsanaCollection<AsanaProject>();
        [JsonProperty(PropertyName = "projects")]
		private List<AsanaProject> _deserializedProjects;

		/// <summary>
        /// Accesses the /workspaces/{id}/projects endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaProject>> GetProjects(System.Nullable<bool> archived = null, string optFields = null)
		{
			var request = new RestRequest("/workspaces/{id}/projects", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(archived, null))
				request.AddParameter("archived", archived.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaProject>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Projects, null))
				{
					Projects = new AsanaCollection<AsanaProject>(resultsCollection);
				}
				else
				{
					Projects.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaProjectCollection.Import(resultsCollection, true);
				return Projects;
			}
		}


		/// <summary>
        /// Accesses the /workspaces/{id}/projects endpoint.
        /// </summary>
        /// <returns></returns>
		public AsanaProject NewProject(AsanaTeam team = null)
		{
			var request = new RestRequest("/workspaces/{id}/projects", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(team, null))
				request.AddParameter("team", team.ToString());
			return new AsanaProject {CreateRequest = request, AsanaHost = AsanaHost}; // TODO: random minus ID ?
		}

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaTag> Tags = new AsanaCollection<AsanaTag>();
        [JsonProperty(PropertyName = "tags")]
		private List<AsanaTag> _deserializedTags;

		/// <summary>
        /// Accesses the /workspaces/{id}/tags endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaTag>> GetTags(string optFields = null)
		{
			var request = new RestRequest("/workspaces/{id}/tags", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTag>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Tags, null))
				{
					Tags = new AsanaCollection<AsanaTag>(resultsCollection);
				}
				else
				{
					Tags.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaTagCollection.Import(resultsCollection, true);
				return Tags;
			}
		}


		/// <summary>
        /// Accesses the /workspaces/{id}/tags endpoint.
        /// </summary>
        /// <returns></returns>
		public AsanaTag NewTag()
		{
			var request = new RestRequest("/workspaces/{id}/tags", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			return new AsanaTag {CreateRequest = request, AsanaHost = AsanaHost}; // TODO: random minus ID ?
		}

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaTask> Tasks = new AsanaCollection<AsanaTask>();
        [JsonProperty(PropertyName = "tasks")]
		private List<AsanaTask> _deserializedTasks;

		/// <summary>
        /// Accesses the /workspaces/{id}/tasks endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaTask>> GetTasks(AsanaUser assignee = null, System.Nullable<System.DateTime> completedSince = null, System.Nullable<System.DateTime> modifiedSince = null, string optFields = null, bool assigneeIsMe = false, bool completedSinceIsNow = false)
		{
			var request = new RestRequest("/workspaces/{id}/tasks", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (assigneeIsMe)
				request.AddParameter("assignee", "me");
			else
			if (!ReferenceEquals(assignee, null))
				request.AddParameter("assignee", assignee.ToString());
			if (completedSinceIsNow)
				request.AddParameter("completed_since", "now");
			else
			if (!ReferenceEquals(completedSince, null))
				request.AddParameter("completed_since", completedSince.ToString());
			if (!ReferenceEquals(modifiedSince, null))
				request.AddParameter("modified_since", modifiedSince.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTask>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Tasks, null))
				{
					Tasks = new AsanaCollection<AsanaTask>(resultsCollection);
				}
				else
				{
					Tasks.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaTaskCollection.Import(resultsCollection, true);
				return Tasks;
			}
		}


		/// <summary>
        /// Accesses the /workspaces/{id}/tasks endpoint.
        /// </summary>
        /// <returns></returns>
		public AsanaTask NewTask(AsanaTaskMembership[] memberships = null)
		{
			var request = new RestRequest("/workspaces/{id}/tasks", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(memberships, null))
				request.AddParameter("memberships", memberships.ToString());
			return new AsanaTask {CreateRequest = request, AsanaHost = AsanaHost}; // TODO: random minus ID ?
		}

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaReadOnlyCollection<AsanaTeam> Teams = new AsanaReadOnlyCollection<AsanaTeam>();
        [JsonProperty(PropertyName = "teams")]
		private List<AsanaTeam> _deserializedTeams;

		/// <summary>
        /// Accesses the /organizations/{id}/teams endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaReadOnlyCollection<AsanaTeam>> GetTeams(string optFields = null)
		{
			var request = new RestRequest("/organizations/{id}/teams", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTeam>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Teams, null))
				{
					Teams = new AsanaReadOnlyCollection<AsanaTeam>(resultsCollection);
				}
				else
				{
					Teams.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaTeamCollection.Import(resultsCollection, true);
				return Teams;
			}
		}

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaTask> QueryTasks = new AsanaCollection<AsanaTask>();
        [JsonProperty(PropertyName = "query_tasks")]
		private List<AsanaTask> _deserializedQueryTasks;

		/// <summary>
        /// Accesses the /workspaces/{id}/typeahead endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaTask>> GetQueryTasks(string query, System.Nullable<int> limit = null, string optFields = null)
		{
			var request = new RestRequest("/workspaces/{id}/typeahead", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("type", "task");
			request.AddParameter("query", query.ToString());
			if (!ReferenceEquals(limit, null))
				request.AddParameter("limit", limit.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTask>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(QueryTasks, null))
				{
					QueryTasks = new AsanaCollection<AsanaTask>(resultsCollection);
				}
				else
				{
					QueryTasks.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaTaskCollection.Import(resultsCollection, true);
				return QueryTasks;
			}
		}

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaUser> QueryUsers = new AsanaCollection<AsanaUser>();
        [JsonProperty(PropertyName = "query_users")]
		private List<AsanaUser> _deserializedQueryUsers;

		/// <summary>
        /// Accesses the /workspaces/{id}/typeahead endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaUser>> GetQueryUsers(string query, System.Nullable<int> limit = null, string optFields = null)
		{
			var request = new RestRequest("/workspaces/{id}/typeahead", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("type", "user");
			request.AddParameter("query", query.ToString());
			if (!ReferenceEquals(limit, null))
				request.AddParameter("limit", limit.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaUser>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(QueryUsers, null))
				{
					QueryUsers = new AsanaCollection<AsanaUser>(resultsCollection);
				}
				else
				{
					QueryUsers.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaUserCollection.Import(resultsCollection, true);
				return QueryUsers;
			}
		}

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaProject> QueryProjects = new AsanaCollection<AsanaProject>();
        [JsonProperty(PropertyName = "query_projects")]
		private List<AsanaProject> _deserializedQueryProjects;

		/// <summary>
        /// Accesses the /workspaces/{id}/typeahead endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaProject>> GetQueryProjects(string query, System.Nullable<int> limit = null, string optFields = null)
		{
			var request = new RestRequest("/workspaces/{id}/typeahead", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("type", "project");
			request.AddParameter("query", query.ToString());
			if (!ReferenceEquals(limit, null))
				request.AddParameter("limit", limit.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaProject>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(QueryProjects, null))
				{
					QueryProjects = new AsanaCollection<AsanaProject>(resultsCollection);
				}
				else
				{
					QueryProjects.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaProjectCollection.Import(resultsCollection, true);
				return QueryProjects;
			}
		}

		// TODO: there's a useless check for whether this is NULL, but it shouldn't be useless if we can "new" this during manual object creation
		public AsanaCollection<AsanaTag> QueryTags = new AsanaCollection<AsanaTag>();
        [JsonProperty(PropertyName = "query_tags")]
		private List<AsanaTag> _deserializedQueryTags;

		/// <summary>
        /// Accesses the /workspaces/{id}/typeahead endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaTag>> GetQueryTags(string query, System.Nullable<int> limit = null, string optFields = null)
		{
			var request = new RestRequest("/workspaces/{id}/typeahead", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("type", "tag");
			request.AddParameter("query", query.ToString());
			if (!ReferenceEquals(limit, null))
				request.AddParameter("limit", limit.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);

			using (var stringReader = new StringReader(response.Content)) // raw content as string
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
                if (!ReferenceEquals(jsonData.Errors, null)) // || !string.IsNullOrEmpty(jsonData.Errors.Message)
                    throw new Exception("A remote error has occured: " + jsonData.Errors.First().Message);
				
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTag>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(QueryTags, null))
				{
					QueryTags = new AsanaCollection<AsanaTag>(resultsCollection);
				}
				else
				{
					QueryTags.Import(resultsCollection);
				}

				AsanaHost.Cache.AsanaTagCollection.Import(resultsCollection, true);
				return QueryTags;
			}
		}

		// makes a dummy save action; TODO: remove this
		public async Task<AsanaWorkspace> Save()
		{
			return this;
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			// Load Projects //
			if (!ReferenceEquals(_deserializedProjects, null))
			{
				if (ReferenceEquals(Projects, null))
				{
					// first deserialization
					Projects = new AsanaCollection<AsanaProject>(_deserializedProjects);
					if (_deserializedProjects.Any())
						OnPropertyChanged("Projects");
				}
				else
				{
					var anythingChanged = Projects.Import(_deserializedProjects);
					if (anythingChanged)
						OnPropertyChanged("Projects");
				}

				// cleanup
				_deserializedProjects = null;
			}
			else if (ReferenceEquals(Projects, null))
			{
				Projects = new AsanaCollection<AsanaProject>();
			}
			// ----------------------------- //

			// Load Tags //
			if (!ReferenceEquals(_deserializedTags, null))
			{
				if (ReferenceEquals(Tags, null))
				{
					// first deserialization
					Tags = new AsanaCollection<AsanaTag>(_deserializedTags);
					if (_deserializedTags.Any())
						OnPropertyChanged("Tags");
				}
				else
				{
					var anythingChanged = Tags.Import(_deserializedTags);
					if (anythingChanged)
						OnPropertyChanged("Tags");
				}

				// cleanup
				_deserializedTags = null;
			}
			else if (ReferenceEquals(Tags, null))
			{
				Tags = new AsanaCollection<AsanaTag>();
			}
			// ----------------------------- //

			// Load Tasks //
			if (!ReferenceEquals(_deserializedTasks, null))
			{
				if (ReferenceEquals(Tasks, null))
				{
					// first deserialization
					Tasks = new AsanaCollection<AsanaTask>(_deserializedTasks);
					if (_deserializedTasks.Any())
						OnPropertyChanged("Tasks");
				}
				else
				{
					var anythingChanged = Tasks.Import(_deserializedTasks);
					if (anythingChanged)
						OnPropertyChanged("Tasks");
				}

				// cleanup
				_deserializedTasks = null;
			}
			else if (ReferenceEquals(Tasks, null))
			{
				Tasks = new AsanaCollection<AsanaTask>();
			}
			// ----------------------------- //

			// Load Teams //
			if (!ReferenceEquals(_deserializedTeams, null))
			{
				if (ReferenceEquals(Teams, null))
				{
					// first deserialization
					Teams = new AsanaReadOnlyCollection<AsanaTeam>(_deserializedTeams);
					if (_deserializedTeams.Any())
						OnPropertyChanged("Teams");
				}
				else
				{
					var anythingChanged = Teams.Import(_deserializedTeams);
					if (anythingChanged)
						OnPropertyChanged("Teams");
				}

				// cleanup
				_deserializedTeams = null;
			}
			else if (ReferenceEquals(Teams, null))
			{
				Teams = new AsanaReadOnlyCollection<AsanaTeam>();
			}
			// ----------------------------- //

			// Load QueryTasks //
			if (!ReferenceEquals(_deserializedQueryTasks, null))
			{
				if (ReferenceEquals(QueryTasks, null))
				{
					// first deserialization
					QueryTasks = new AsanaCollection<AsanaTask>(_deserializedQueryTasks);
					if (_deserializedQueryTasks.Any())
						OnPropertyChanged("QueryTasks");
				}
				else
				{
					var anythingChanged = QueryTasks.Import(_deserializedQueryTasks);
					if (anythingChanged)
						OnPropertyChanged("QueryTasks");
				}

				// cleanup
				_deserializedQueryTasks = null;
			}
			else if (ReferenceEquals(QueryTasks, null))
			{
				QueryTasks = new AsanaCollection<AsanaTask>();
			}
			// ----------------------------- //

			// Load QueryUsers //
			if (!ReferenceEquals(_deserializedQueryUsers, null))
			{
				if (ReferenceEquals(QueryUsers, null))
				{
					// first deserialization
					QueryUsers = new AsanaCollection<AsanaUser>(_deserializedQueryUsers);
					if (_deserializedQueryUsers.Any())
						OnPropertyChanged("QueryUsers");
				}
				else
				{
					var anythingChanged = QueryUsers.Import(_deserializedQueryUsers);
					if (anythingChanged)
						OnPropertyChanged("QueryUsers");
				}

				// cleanup
				_deserializedQueryUsers = null;
			}
			else if (ReferenceEquals(QueryUsers, null))
			{
				QueryUsers = new AsanaCollection<AsanaUser>();
			}
			// ----------------------------- //

			// Load QueryProjects //
			if (!ReferenceEquals(_deserializedQueryProjects, null))
			{
				if (ReferenceEquals(QueryProjects, null))
				{
					// first deserialization
					QueryProjects = new AsanaCollection<AsanaProject>(_deserializedQueryProjects);
					if (_deserializedQueryProjects.Any())
						OnPropertyChanged("QueryProjects");
				}
				else
				{
					var anythingChanged = QueryProjects.Import(_deserializedQueryProjects);
					if (anythingChanged)
						OnPropertyChanged("QueryProjects");
				}

				// cleanup
				_deserializedQueryProjects = null;
			}
			else if (ReferenceEquals(QueryProjects, null))
			{
				QueryProjects = new AsanaCollection<AsanaProject>();
			}
			// ----------------------------- //

			// Load QueryTags //
			if (!ReferenceEquals(_deserializedQueryTags, null))
			{
				if (ReferenceEquals(QueryTags, null))
				{
					// first deserialization
					QueryTags = new AsanaCollection<AsanaTag>(_deserializedQueryTags);
					if (_deserializedQueryTags.Any())
						OnPropertyChanged("QueryTags");
				}
				else
				{
					var anythingChanged = QueryTags.Import(_deserializedQueryTags);
					if (anythingChanged)
						OnPropertyChanged("QueryTags");
				}

				// cleanup
				_deserializedQueryTags = null;
			}
			else if (ReferenceEquals(QueryTags, null))
			{
				QueryTags = new AsanaCollection<AsanaTag>();
			}
			// ----------------------------- //

		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}

		public bool HasLocalChanges
		{
			get 
			{
				var hasChanges = false;
				return hasChanges;
			}
		}
	} // END class: AsanaWorkspace

	public partial class AsanaTeam : AsanaResource
	{
		public string Name
		{
			get 
			{
				return _remoteName;
			}
        }
		
		[JsonProperty("name")]
        private string _remoteName
		{
			get 
			{
				return _remoteValueName;
			}
			set
			{
				if (_remoteValueName != value)
				{
					_remoteValueName = value;
					OnPropertyChanged("Name");
				}
			}
		}
		private string _remoteValueName;

		public AsanaWorkspace Organization
		{
			get 
			{
				return _remoteOrganization;
			}
        }
		
		[JsonProperty("organization")]
        private AsanaWorkspace _remoteOrganization
		{
			get 
			{
				return _remoteValueOrganization;
			}
			set
			{
				if (_remoteValueOrganization != value)
				{
					_remoteValueOrganization = value;
					OnPropertyChanged("Organization");
				}
			}
		}
		private AsanaWorkspace _remoteValueOrganization;

		// makes a dummy save action; TODO: remove this
		public async Task<AsanaTeam> Save()
		{
			return this;
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}

		public bool HasLocalChanges
		{
			get 
			{
				var hasChanges = false;
				return hasChanges;
			}
		}
	} // END class: AsanaTeam

	public partial class AsanaAttachment : AsanaResource
	{
		public System.Nullable<System.DateTime> CreatedAt
		{
			get 
			{
				return _remoteCreatedAt;
			}
        }
		
		[JsonProperty("created_at")]
        private System.Nullable<System.DateTime> _remoteCreatedAt
		{
			get 
			{
				return _remoteValueCreatedAt;
			}
			set
			{
				if (_remoteValueCreatedAt != value)
				{
					_remoteValueCreatedAt = value;
					OnPropertyChanged("CreatedAt");
				}
			}
		}
		private System.Nullable<System.DateTime> _remoteValueCreatedAt;

		public string DownloadUrl
		{
			get 
			{
				return _remoteDownloadUrl;
			}
        }
		
		[JsonProperty("download_url")]
        private string _remoteDownloadUrl
		{
			get 
			{
				return _remoteValueDownloadUrl;
			}
			set
			{
				if (_remoteValueDownloadUrl != value)
				{
					_remoteValueDownloadUrl = value;
					OnPropertyChanged("DownloadUrl");
				}
			}
		}
		private string _remoteValueDownloadUrl;

		public string Host
		{
			get 
			{
				return _remoteHost;
			}
        }
		
		[JsonProperty("host")]
        private string _remoteHost
		{
			get 
			{
				return _remoteValueHost;
			}
			set
			{
				if (_remoteValueHost != value)
				{
					_remoteValueHost = value;
					OnPropertyChanged("Host");
				}
			}
		}
		private string _remoteValueHost;

		public string Name
		{
			get 
			{
				return _remoteName;
			}
        }
		
		[JsonProperty("name")]
        private string _remoteName
		{
			get 
			{
				return _remoteValueName;
			}
			set
			{
				if (_remoteValueName != value)
				{
					_remoteValueName = value;
					OnPropertyChanged("Name");
				}
			}
		}
		private string _remoteValueName;

		public AsanaTask Parent
		{
			get 
			{
				return _remoteParent;
			}
        }
		
		[JsonProperty("parent")]
        private AsanaTask _remoteParent
		{
			get 
			{
				return _remoteValueParent;
			}
			set
			{
				if (_remoteValueParent != value)
				{
					_remoteValueParent = value;
					OnPropertyChanged("Parent");
				}
			}
		}
		private AsanaTask _remoteValueParent;

		public string ViewUrl
		{
			get 
			{
				return _remoteViewUrl;
			}
        }
		
		[JsonProperty("view_url")]
        private string _remoteViewUrl
		{
			get 
			{
				return _remoteValueViewUrl;
			}
			set
			{
				if (_remoteValueViewUrl != value)
				{
					_remoteValueViewUrl = value;
					OnPropertyChanged("ViewUrl");
				}
			}
		}
		private string _remoteValueViewUrl;

		// makes a dummy save action; TODO: remove this
		public async Task<AsanaAttachment> Save()
		{
			return this;
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}

		public bool HasLocalChanges
		{
			get 
			{
				var hasChanges = false;
				return hasChanges;
			}
		}
	} // END class: AsanaAttachment

} // END: namespace
