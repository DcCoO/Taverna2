using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{

    public int x0, y0, xf, yf;
    public Vertex[,] g;

    public Transform player;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(createGraph());
        //StartCoroutine(BFS());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos = new Vector2(Mathf.Floor(pos.x) + 0.5f, Mathf.Floor(pos.y) + 0.5f);





            //  |0| | | | | | | | |9|
            //-5          0         5
        }
    }

    IEnumerator createGraph()
    {
        yield return null;
        g = new Vertex[18, 10];
        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                g[i, j] = new Vertex(i, j);

            }
            yield return null;
        }

        //tirando so uma mesa
        g[11, 4] = g[12, 4] =  g[11, 3] = g[12,3] = null;

        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if(g[i,j] == null) { yield return null; continue; }
                if (j != 0) g[i, j].Add(g[i, j - 1]);
                if (j != g.GetLength(1) - 1) g[i, j].Add(g[i, j + 1]);
                if (i != 0) g[i, j].Add(g[i - 1, j]);
                if (i != g.GetLength(0) - 1) g[i, j].Add(g[i + 1, j]);
                yield return null;
            }
            yield return null;
        }


        /*
        IEnumerator date () {
            float x = -4.5f, y = 8.5f;
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    GameObject g = new GameObject();
                    g.transform.position = new Vector2(x + j, y - i);
                    int num = (i * 10 + j);
                    g.name = "grid " + num;
                    SpriteRenderer sr = g.AddComponent<SpriteRenderer>();
                    sr.sprite = grid2;
                    yield return null;
                }
            }
        }*/

        yield return StartCoroutine(BFS());
    }

    int map(int i, int j)
    {
        return i * 10 + j;
    }

    IEnumerator BFS()
    {

        Vertex source = g[y0, x0];
        Queue<Vertex> Q = new Queue<Vertex>();
        Q.Enqueue(source);

        Dictionary<int, Vertex> pai = new Dictionary<int, Vertex>();
        bool[,] visit = new bool[18, 10];

        while (Q.Count > 0)
        {
            int s1 = Q.Count;
            Vertex u = Q.Dequeue();
            
            //print("visitando vertice (" + u.i + ", " + u.j + ") de estado " + visit[u.i, u.j]);
            visit[u.i, u.j] = true;
            foreach (Vertex v in u.adj)
            {
                if (!visit[v.i, v.j])
                {
                    Q.Enqueue(v);
                    visit[v.i, v.j] = true;
                    pai[v.map()] = u;

                    //se for o final, encerra
                    if (v.map() == map(yf, xf)) goto Path;
                    
                    yield return null;
                }
            }
            yield return null;
        }

    Path:

        //print("calculou");

        Vertex target = g[yf, xf];
        Stack<Vertex> pilha = new Stack<Vertex>();
        while(target.map() != source.map())
        {
            pilha.Push(target);
            target = pai[target.map()];
            yield return null;
        }
        pilha.Push(source);

        //percorrer
        while(pilha.Count > 0)
        {
            Vertex dir = pilha.Pop();
            for(float t = 0; t < 1f; t += Time.deltaTime)
            {
                Vector2 begin = transform.position, end = dir.worldPos;
                transform.position = Vector2.Lerp(begin, end, t);

                yield return null;
            }
            yield return null;
        }
    }
}

public class Vertex {
    public int i, j;
    public Vector2 worldPos;
    public Vertex(int i, int j)
    {
        this.i = i; this.j = j;
        worldPos = new Vector2(-4.5f + j, 8.5f - i);
    }

    public void Add(Vertex v)
    {
        if (v != null) adj.Add(v);
    }

    public int map()
    {
        return i * 10 + j;
    }

    public string PrintMe()
    {
        return i + "," + j;
    }
    public void PrintAdj()
    {
        string s = "";
        foreach(Vertex v in adj)
        {
            s = s + "(" + v.PrintMe() + ") ";
        }
        Debug.Log(s);
    }
    public List<Vertex> adj = new List<Vertex>();
}
    