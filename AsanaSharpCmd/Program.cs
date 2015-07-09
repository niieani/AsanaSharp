using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AsanaSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AsanaSharpCmd
{
    class Program
    {
        public static async Task TestAsana()
        {
            var asana = new Asana();
            var me = await asana.GetMe();
            var workspaces = await asana.GetWorkspaces();
            var firstWorkspace = workspaces.First();
            var lastWorkspace = workspaces.Last();
	        var projects = await lastWorkspace.GetProjects();
	        var firstProject = projects.First();
	        var firstSection = (await firstProject.GetSections()).First();
			var myNewTask = me.NewTask(firstWorkspace);
	        lastWorkspace.NewTask(new [] {new AsanaTaskMembership(firstProject, firstSection)});
			myNewTask.Name = "Hey Babe!";
			await myNewTask.AddProject(firstProject, firstSection);

			//            var myTasks = await firstWorkspace.GetMyTasks();
			var newTask = lastWorkspace.NewTask();
            newTask.Assignee = me;
            newTask.Name = "Hey yeah!";
            newTask.HtmlNotes = "Yoyoyo.";
            await newTask.Save();
            newTask.Name = "Just changing names";
            await newTask.Save();
            var newProject = lastWorkspace.NewProject();
            newProject.Name = "The Test Project";
            newTask.Projects.Add(newProject);
            await newTask.Save();
        }

        static void Main(string[] args)
        {
            TestAsana().GetAwaiter().GetResult();
            /*
            var asana = new AsanaSharp.AsanaSharp();
            asana.Deserialize();
            */

            /*
            var asana = new Asana();
            var workspaces = asana.GetWorkspaces().Result;

            var tasks = workspaces.First().GetMyTasks(completedSinceIsNow: true, optFields: "name,projects,projects.name,assignee,assignee_status").Result;
//            var tasks2 = workspaces.First().GetMyTasks(completedSinceIsNow: true, optFields: "name,projects,projects.name,assignee,assignee_status").Result;
            var tasksWithProjects = tasks.Where(t => t.Projects.Any());

            workspaces.First().Name = "Wtf";
            //            var workspaces2 = asana.GetWorkspaces().Result;
            //            workspaces.Last().Name = "Wtf2";
            //            var dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //            dcr.DefaultMembersSearchFlags |= System.Reflection.BindingFlags.NonPublic;
            //            jss.ContractResolver = dcr;

            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var writer = new JsonTextWriter(sw);
            asana.JsonSerializer.Serialize(writer, workspaces);
            var serialized = sw.ToString();
            JsonConvert.SerializeObject(workspaces.First(), Formatting.Indented, new JsonSerializerSettings
            {
                //ContractResolver = new SelectedContractResolver(elementsToSerialize),
//                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize, //.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceResolver = new AsanaReferenceResolver(),
                //Context = new StreamingContext(StreamingContextStates.All, new JsonLinkedContext(asana)),
                //                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new AtaResolver(),
            });
            */
            //            Remoteable<int> testRemoteable = 2;
            //            int tat = (int)testRemoteable;

            //            asana.DeserializeCustomReferenceResolver();
            //Console.ReadLine();
            //            AsanaSharp.Program.Main();
        }
    }
}
