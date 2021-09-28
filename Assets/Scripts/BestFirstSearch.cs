using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestFirstSearch : MonoBehaviour
{
    Heuristic h, h2;
    float Dist = 0, Dist2 = 0;
    Color black, red, green;
    void Start()
    {
        black = new Color32(0, 0, 0, 255);
        red = new Color32(255, 0, 0, 255);
        green = new Color32(0, 255, 0, 255);
        h = new Heuristic();
        h2 = new Heuristic();
    }
    void Update()
    {
        if (!Barriers.done)
        {
            h.Clear();
            h2.Clear();
        }
    }
    void FixedUpdate()
    {
        if (h.Count() > 0)
        {
            h.GetI(0).GetComponent<Advance>().start = true;
            h.Remove(0);
        }
    }
    public void Adv2(int y, int x)
    {
        Image img = Barriers.t.GetImage(y, x);
        int ex = Barriers.ex;
        int ey = Barriers.ey;
        Dist = 0;
        if (Barriers.t.Left(y, x) != null && Barriers.t.Left(y, x).color != black && !Barriers.t.Left(y, x).GetComponent<Advance>().visited && Barriers.t.Left(y, x).color != red)
        {
            int sx = Barriers.t.GetX(Barriers.t.Left(y, x));
            int sy = Barriers.t.GetY(Barriers.t.Left(y, x));
            Dist = Mathf.Sqrt(Mathf.Pow((sx - ex), 2) + Mathf.Pow((sy - ey), 2));
            if (!h.Includes(Barriers.t.Left(y, x)))
            {
                h.AddImageH(Barriers.t.Left(y, x), Dist);
            }
        }
        if (Barriers.t.Right(y, x) != null && Barriers.t.Right(y, x).color != black && !Barriers.t.Right(y, x).GetComponent<Advance>().visited && Barriers.t.Right(y, x).color != red)
        {
            int sx = Barriers.t.GetX(Barriers.t.Right(y, x));
            int sy = Barriers.t.GetY(Barriers.t.Right(y, x));
            Dist = Mathf.Sqrt(Mathf.Pow((sx - ex), 2) + Mathf.Pow((sy - ey), 2));
            if (!h.Includes(Barriers.t.Right(y, x)))
            {
                h.AddImageH(Barriers.t.Right(y, x), Dist);
            }
        }
        if (Barriers.t.Top(y, x) != null && Barriers.t.Top(y, x).color != black && !Barriers.t.Top(y, x).GetComponent<Advance>().visited && Barriers.t.Top(y, x).color != red)
        {
            int sx = Barriers.t.GetX(Barriers.t.Top(y, x));
            int sy = Barriers.t.GetY(Barriers.t.Top(y, x));
            Dist = Mathf.Sqrt(Mathf.Pow((sx - ex), 2) + Mathf.Pow((sy - ey), 2));
            if (!h.Includes(Barriers.t.Top(y, x)))
            {
                h.AddImageH(Barriers.t.Top(y, x), Dist);
            }
        }
        if (Barriers.t.Bottum(y, x) != null && Barriers.t.Bottum(y, x).color != black && !Barriers.t.Bottum(y, x).GetComponent<Advance>().visited && Barriers.t.Bottum(y, x).color != red)
        {
            int sx = Barriers.t.GetX(Barriers.t.Bottum(y, x));
            int sy = Barriers.t.GetY(Barriers.t.Bottum(y, x));
            Dist = Mathf.Sqrt(Mathf.Pow((sx - ex), 2) + Mathf.Pow((sy - ey), 2));
            if (!h.Includes(Barriers.t.Bottum(y, x)))
            {
                h.AddImageH(Barriers.t.Bottum(y, x), Dist);
            }
        }
        img.GetComponent<Advance>().start = false;
    }
}

class Heuristic
{
    Image img;
    float D;
    List<Heuristic> heu = new List<Heuristic>();
    public Heuristic()
    {
    }
    public Heuristic(Image Img, float d)
    {
        img = Img;
        D = d;
    }
    public void AddImageH(Image img, float D)
    {
        if (heu.Count > 0)
        {
            int ii = heu.Count - 1;
            while (ii >= 0 && heu[ii].D > D)
            {
                ii--;
            }
            ii++;
            heu.Insert(ii, new Heuristic(img, D));
        }
        else
        {
            heu.Add(new Heuristic(img, D));
        }
    }
    public bool Includes(Image I)
    {
        for(int j = 0; j < heu.Count; j++)
        {
            if (heu[j].img == I)
            {
                return true;
            }
        }
        return false;
    }
    public void Clear()
    {
        heu.Clear();
    }
    public float GetD(int i)
    {
        return heu[i].D;
    }
    public Image GetI(int i)
    {
        return heu[i].img;
    }
    public int Count()
    {
        return heu.Count;
    }
    public void Remove(int i)
    {
        heu.Remove(heu[i]);
    }
    public void Show()
    {
        for(int i = 0; i < heu.Count; i++)
        {
            Debug.Log("(" + Barriers.t.GetY(heu[i].img) + ", " + Barriers.t.GetX(heu[i].img) + ") " + heu[i].D);
        }
    }
}
