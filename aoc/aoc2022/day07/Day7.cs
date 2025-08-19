public class Day7
{
    private class Folder
    {
        public Dictionary<string, Folder> SubFolders;
        public Dictionary<string, int> Files;
        public Folder parentFolder;
        public string Name;
        public int totalSize;
        public bool totalSizeCalculated = false;

        public Folder(string name)
        {
            SubFolders = new Dictionary<string, Folder>();
            Files = new Dictionary<string, int>();
            Name = name;
        }

        public int DetermineTotalSize()
        {
            if (totalSizeCalculated)
            {
                return totalSize;
            }
            var size = 0;
            foreach (KeyValuePair<string, Folder> subFolder in SubFolders)
            {
                size += subFolder.Value.DetermineTotalSize();
            }
            foreach (KeyValuePair<string, int> file in Files)
            {
                size += file.Value;
            }
            totalSize = size;
            totalSizeCalculated = true;
            return size;
        }
    }
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day07/p.in"))
        {
            var currentFolder = new Folder("");
            var rootFolder = new Folder("/");
            currentFolder.SubFolders.Add("/", rootFolder);
            var listing = false;
            var allFolders = new List<Folder>();
            allFolders.Add(rootFolder);

            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                string[] cmd = ln.Split(' ');
                if (cmd[0] == "$")
                {
                    listing = false;
                    if (cmd[1] == "cd")
                    {
                        if (cmd[2] == "..")
                        {
                            currentFolder = currentFolder.parentFolder;
                        }
                        else
                        {
                            foreach (KeyValuePair<string, Folder> folder in currentFolder.SubFolders)
                            {
                                if (folder.Key == cmd[2])
                                {
                                    folder.Value.parentFolder = currentFolder;
                                    currentFolder = folder.Value;
                                    break;
                                }
                            }
                        }
                    }
                    else if (cmd[1] == "ls")
                    {
                        listing = true;
                    }
                }
                else if (listing)
                {
                    if (cmd[0] == "dir")
                    {
                        if (currentFolder.SubFolders.ContainsKey(cmd[1]))
                        {
                            currentFolder.SubFolders[cmd[1]].parentFolder = currentFolder;
                        }
                        else
                        {
                            var newFolder = new Folder(cmd[1]);
                            currentFolder.SubFolders.Add(cmd[1], newFolder);
                            allFolders.Add(newFolder);
                        }
                    }
                    else
                    {
                        if (!currentFolder.Files.ContainsKey(cmd[1]))
                        {
                            currentFolder.Files.Add(cmd[1], int.Parse(cmd[0]));
                        }
                    }
                }
            }

            file.Close();

            foreach (var folder in allFolders)
            {
                var folderSize = folder.DetermineTotalSize();
            }
            var unused = 70000000 - rootFolder.DetermineTotalSize();
            var needed = 30000000 - unused;
            var smallest = int.MaxValue;
            foreach (var folder in allFolders)
            {
                if (folder.totalSize >= needed && folder.totalSize < smallest)
                {
                    smallest = folder.totalSize;
                }
            }
            Console.WriteLine(smallest);
        }
    }
}

