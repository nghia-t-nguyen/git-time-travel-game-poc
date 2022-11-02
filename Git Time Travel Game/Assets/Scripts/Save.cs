using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData {
    public bool cat = true;
}

public class Node {
    public string ID;
    public GameData data;
    public LinkedList<Node> children;

    public Node(string anID, GameData aData) {
        ID = anID;
        aData = data;
        children = new LinkedList<Node>();
    }
}

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
    public LinkedList<Branch> branches;
    public Node gitHead;

    public GitTree(Node start) {
        head = start;
        gitHead = start;
        branches = new LinkedList<Branch>();
        branches.AddLast(new Branch("main", start));
    }

    public int checkout(string nodeID){
        foreach (Branch branch in branches) {
            if (branch.name == nodeID)
                gitHead = branch.pointer;
                return 0;
        }

        Node result = findNode(head, nodeID);
        if (result != null) {
            gitHead = result;
            return 0;
        }

        return -1;
    }


    public Node findNode(Node n, string s) {
        if (n.ID == s) {
            return n;
        } else {
            foreach (Node child in n.children) {
                Node result = findNode(child, s);
                if (result != null) {
                    return result;
                }
            }
        }
        return null;
    }
}