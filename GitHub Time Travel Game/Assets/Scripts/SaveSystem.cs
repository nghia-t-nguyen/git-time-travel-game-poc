using System.Collections;
using System.Collections.Generic;

/*
 * This class represents game data to be saved by the user
 */
public class GameData {
    public int time;
    public bool cat;

    public GameData() {
        cat = true;
        time = (int) StopwatchTimer.currentTime;
    }

    public GameData merge(GameData other) {
        return new GameData();
    }
}

/*
 * This class represents a git tree node to save the game data
 */
public class Node {
    public string ID;
    public GameData data;
    public LinkedList<Node> children;
    public static System.Random random = new System.Random();

    public Node(string anID, GameData aData) {
        ID = anID;
        data = aData;
        children = new LinkedList<Node>();
    }

    public Node(GameData aData) {
        ID = random.Next(268435455).ToString("X");
        data = aData;
        children = new LinkedList<Node>();
    }

    public Node merge(Node other) {
        return new Node(new GameData());
    }
}
/*
 * This class represents a git tree branch
 */
public class Branch {
    public string name;
    public Node pointer;

    public Branch(string aName, Node aPointer) {
        name = aName;
        pointer = aPointer;
    }
}

public class GitTree {
    public Node head;
    //public LinkedList<Node> allNodes;
    public LinkedList<Branch> branches;
    public Node gitHead;

    public static readonly Node noNode = new Node("NULL", new GameData());

    public GitTree(Node start) {
        head = start;
        gitHead = start;
        branches = new LinkedList<Branch>();
        branches.AddLast(new Branch("main", start));
    }

    // Input: a string array with each word
    // Precondition: input[0] starts with git
    // Error Codes: -2 node cannot be found; -1 invalid input; 0 successful git command; 1 clear; 2 do nothing
    public int parseString(string input) {
        string[] arguments = input.Split(' ');

        if (arguments[0] == "") {
            return 2;
        } else if (arguments[0] != "git" && arguments[0] != "clear") {
            return -1;

        } else if (arguments[0] == "clear") {
            return 1;

        } else if (arguments[1] == "commit" && arguments.Length == 2) {
            commit(new Node(new GameData()));
            return 0;

        } else if (arguments[1] == "checkout" && arguments.Length == 3) {
            return checkout(arguments[2]);

        } else if (arguments[1] == "merge" && arguments.Length == 3) {
            return merge(gitHead, findNode(head, arguments[2]));

        } else if (arguments[1] == "branch" && arguments.Length == 3) {
            branch(arguments[2]);
            return 0;

        } else {
            return -1;
        }
    }

    public void commit(Node aNode) {
        gitHead.children.AddLast(aNode);
        foreach (Branch branch in branches) {
            if (branch.pointer.ID == gitHead.ID) {
                branch.pointer = aNode;
                break;
            }
        }
        gitHead = aNode;
    }

    public int checkout(string identifier){
        Node result = findNode(head, identifier);
        if (result == noNode)
            return -2;  // Node not found
        else {
            gitHead = result;
            return 0;
        }
    }


    public int merge(Node first, Node second) {
        if (first == second) {
            return -2;
        } else if (first == noNode || second == noNode) {
            return -2;
        } else {
            Node merged = first.merge(second);
            first.children.AddLast(merged);
            second.children.AddLast(merged);
            
            foreach (Branch b in branches) {
                if (b.pointer == first) {
                    b.pointer = merged;
                }
            }

            gitHead = merged;
            return 0;
        }
    }

    public void branch(string branchName) {
        branches.AddLast(new Branch(branchName, gitHead));
    }

    //BFS search
    private Node findNode(Node n, string s) {
        foreach (Branch b in branches) {
            if (b.name == s) {
                return b.pointer;
            }
        }

        if (n.ID == s)
            return n;

        Queue<Node> q = new Queue<Node>();
        LinkedList<Node> visited = new LinkedList<Node>();
        visited.AddLast(n);
        q.Enqueue(n);
        
        while(q.Count > 0) {
            Node dq = q.Dequeue();
            foreach (Node c in dq.children) {
                if (c.ID == s)
                    return c;
                if (visited.Contains(n) != true) {
                    visited.AddLast(n);
                    q.Enqueue(n);
                }
            }

        }
        
        return noNode;
    }

    public override string ToString() {
        string ret = printBFS(head);

        ret += "Branches:\n";
        foreach (Branch b in branches) {
            ret += "-" + b.name + ": " + b.pointer.ID + "\n";
        }

        ret += "\n";
        ret += "Head: " + gitHead.ID + "\n";

        return ret;
    }

    private string printBFS(Node aNode) {
        string ret = "";

        Queue<Node> q = new Queue<Node>();
        LinkedList<Node> visited = new LinkedList<Node>();
        visited.AddLast(aNode);
        q.Enqueue(aNode);

        while(q.Count > 0) {
            Node dq = q.Dequeue();
            ret += "NodeID: " + dq.ID + "\n";
            ret += "Children:\n";
            foreach (Node n in dq.children) {
                ret += "-" + n.ID + "\n";
            }
            ret += "\n";

            foreach (Node n in dq.children) {
                if (visited.Contains(n) != true) {
                    visited.AddLast(n);
                    q.Enqueue(n);
                }
            }

        }
        return ret;
    }

}
