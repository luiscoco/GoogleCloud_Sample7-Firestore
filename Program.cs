using Google.Api;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

string project = "focus-cache-387205";

//InitializeProjectId(project);
AddData1(project).Wait();
AddData2(project).Wait();
RetrieveAllDocuments(project).Wait();

void InitializeProjectId(string project)
{
    // [START firestore_setup_client_create_with_project_id]
    FirestoreDb db = FirestoreDb.Create(project);
    Console.WriteLine("Created Cloud Firestore client with project ID: {0}", project);
    // [END firestore_setup_client_create_with_project_id]
}

async Task AddData1(string project)
{
    FirestoreDb db = FirestoreDb.Create(project);
    // [START firestore_setup_dataset_pt1]
    DocumentReference docRef = db.Collection("users").Document("alovelace");
    Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "First", "Ada" },
                { "Last", "Lovelace" },
                { "Born", 1815 }
            };
    await docRef.SetAsync(user);
    // [END firestore_setup_dataset_pt1]
    Console.WriteLine("Added data to the alovelace document in the users collection.");
}

async Task AddData2(string project)
{
    FirestoreDb db = FirestoreDb.Create(project);
    // [START firestore_setup_dataset_pt2]
    DocumentReference docRef = db.Collection("users").Document("aturing");
    Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "First", "Alan" },
                { "Middle", "Mathison" },
                { "Last", "Turing" },
                { "Born", 1912 }
            };
    await docRef.SetAsync(user);
    // [END firestore_setup_dataset_pt2]
    Console.WriteLine("Added data to the aturing document in the users collection.");
}

async Task RetrieveAllDocuments(string project)
{
    FirestoreDb db = FirestoreDb.Create(project);
    // [START firestore_setup_dataset_read]
    CollectionReference usersRef = db.Collection("users");
    QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
    foreach (DocumentSnapshot document in snapshot.Documents)
    {
        Console.WriteLine("User: {0}", document.Id);
        Dictionary<string, object> documentDictionary = document.ToDictionary();
        Console.WriteLine("First: {0}", documentDictionary["First"]);
        if (documentDictionary.ContainsKey("Middle"))
        {
            Console.WriteLine("Middle: {0}", documentDictionary["Middle"]);
        }
        Console.WriteLine("Last: {0}", documentDictionary["Last"]);
        Console.WriteLine("Born: {0}", documentDictionary["Born"]);
        Console.WriteLine();
    }
    // [END firestore_setup_dataset_read]
}

