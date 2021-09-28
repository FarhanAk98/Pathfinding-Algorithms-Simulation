using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Advance : MonoBehaviour
{
    [HideInInspector]
    public bool start = false;
    [HideInInspector]
    public int xp = -1, yp = -1, x, y, cost;
    [HideInInspector]
    public bool Dest = false, visited = false, visited2 = false, Checked = false;
    Color black, blue, yellow, darkGreen, red, pink, green;
    void Start()
    {
        blue = new Color32(0, 230, 255, 255);
        darkGreen = new Color32(0, 100, 0, 255);
        yellow = new Color32(255, 255, 0, 255);
        red = new Color32(255, 0, 0, 255);
        green = new Color32(0, 255, 0, 255);
        black = new Color32(0, 0, 0, 255);
        pink = new Color32(255, 0, 255, 255);
        x = Barriers.t.GetX(transform.GetComponent<Image>());
        y = Barriers.t.GetY(transform.GetComponent<Image>());
        cost = -1;
    }
    void Update()
    {
        if (transform.GetComponent<Image>().color != blue && 
            transform.GetComponent<Image>().color != yellow && 
            transform.GetComponent<Image>().color != pink && 
            transform.GetComponent<Image>().color != red && 
            transform.GetComponent<Image>().color != green)
        {
            if (visited && !visited2)
            {
                transform.GetComponent<Image>().color = Color.Lerp(Barriers.t.GetImage(y, x).color, blue, 2f * Time.deltaTime);
            }
            else if(visited2 && !visited)
            {
                transform.GetComponent<Image>().color = Color.Lerp(Barriers.t.GetImage(y, x).color, pink, 2f * Time.deltaTime);
            }
            else if(start && Barriers.found)
            {
                if (Dest)
                {
                    transform.GetComponent<Image>().color = Color.Lerp(Barriers.t.GetImage(y, x).color, pink, 2f * Time.deltaTime);
                }
                else
                {
                    transform.GetComponent<Image>().color = Color.Lerp(Barriers.t.GetImage(y, x).color, blue, 2f * Time.deltaTime);
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (Checked)
        {
            GetCost();
        }
        if (Dest && Barriers.done)
        {
            Image im = this.GetComponent<Image>();
            int c = 0;
            if (Barriers.t.Left(y, x) != null && Barriers.t.Left(y, x).GetComponent<Advance>().cost > c && Barriers.t.Left(y, x).GetComponent<Advance>().visited)
            {
                im = Barriers.t.Left(y, x);
                c = Barriers.t.Left(y, x).GetComponent<Advance>().cost;
            }
            if (Barriers.t.Right(y, x) != null && Barriers.t.Right(y, x).GetComponent<Advance>().cost > c && Barriers.t.Right(y, x).GetComponent<Advance>().visited)
            {
                im = Barriers.t.Right(y, x);
                c = Barriers.t.Right(y, x).GetComponent<Advance>().cost;
            }
            if (Barriers.t.Top(y, x) != null && Barriers.t.Top(y, x).GetComponent<Advance>().cost > c && Barriers.t.Top(y, x).GetComponent<Advance>().visited)
            {
                im = Barriers.t.Top(y, x);
                c = Barriers.t.Top(y, x).GetComponent<Advance>().cost;
            }
            if (Barriers.t.Bottum(y, x) != null && Barriers.t.Bottum(y, x).GetComponent<Advance>().cost > c && Barriers.t.Bottum(y, x).GetComponent<Advance>().visited)
            {
                im = Barriers.t.Bottum(y, x);
                c = Barriers.t.Bottum(y, x).GetComponent<Advance>().cost;
            }
            if (c > 0)
            {
                Image im2 = this.GetComponent<Image>();
                if (Barriers.Dir == 1)
                {
                    int c2 = 0;
                    if (Barriers.t.Left(y, x) != null && Barriers.t.Left(y, x).GetComponent<Advance>().cost > c2 && Barriers.t.Left(y, x).GetComponent<Advance>().visited2)
                    {
                        im2 = Barriers.t.Left(y, x);
                        c2 = Barriers.t.Left(y, x).GetComponent<Advance>().cost;
                    }
                    if (Barriers.t.Right(y, x) != null && Barriers.t.Right(y, x).GetComponent<Advance>().cost > c2 && Barriers.t.Right(y, x).GetComponent<Advance>().visited2)
                    {
                        im2 = Barriers.t.Right(y, x);
                        c2 = Barriers.t.Right(y, x).GetComponent<Advance>().cost;
                    }
                    if (Barriers.t.Top(y, x) != null && Barriers.t.Top(y, x).GetComponent<Advance>().cost > c2 && Barriers.t.Top(y, x).GetComponent<Advance>().visited2)
                    {
                        im2 = Barriers.t.Top(y, x);
                        c2 = Barriers.t.Top(y, x).GetComponent<Advance>().cost;
                    }
                    if (Barriers.t.Bottum(y, x) != null && Barriers.t.Bottum(y, x).GetComponent<Advance>().cost > c2 && Barriers.t.Bottum(y, x).GetComponent<Advance>().visited2)
                    {
                        im2 = Barriers.t.Bottum(y, x);
                        c2 = Barriers.t.Bottum(y, x).GetComponent<Advance>().cost;
                    }
                    TurnYellow(im2);
                    this.GetComponent<Image>().color = yellow;
                }
                Debug.Log(c);
                TurnYellow(im);
                Barriers.done = false;
                Barriers.found = true;
            }
        }
        if (!Dest || (Dest && Barriers.Dir == 1))
        {
            if (start && Barriers.done && !Barriers.found)
            {
                if ((!Dest && !visited) || (Dest && !visited2))
                {
                    Adv();
                }
                else
                {
                    if (Barriers.Alg == 0)
                    {
                        Barriers.Dig.Adv2(y, x);
                    }
                    else if (Barriers.Alg == 1)
                    {
                        Barriers.Ast.Adv2(y, x);
                    }
                    else if (Barriers.Alg == 2)
                    {
                        Barriers.bfs.Adv2(y, x);
                    }
                    else if (Barriers.Alg == 3)
                    {
                        Barriers.befs.Adv2(y, x);
                    }
                }
                Checked = true;
            }
        }
    }
    void GetCost()
    {
        if (Barriers.t.Left(y, x) != null && ((Barriers.t.Left(y, x).GetComponent<Advance>().visited && !Dest) || (Barriers.t.Left(y, x).GetComponent<Advance>().visited2 && Dest)) && Barriers.t.Left(y, x).GetComponent<Advance>().cost != -1 &&
            (Barriers.t.Left(y, x).GetComponent<Advance>().cost < cost - 1 || cost == -1))
        {
            cost = Barriers.t.Left(y, x).GetComponent<Advance>().cost + 1;
            yp = y;
            xp = x - 1;
        }
        if (Barriers.t.Right(y, x) != null && ((Barriers.t.Right(y, x).GetComponent<Advance>().visited && !Dest) || (Barriers.t.Right(y, x).GetComponent<Advance>().visited2 && Dest)) && Barriers.t.Right(y, x).GetComponent<Advance>().cost != -1 && 
            (Barriers.t.Right(y, x).GetComponent<Advance>().cost < cost - 1 || cost == -1))
        {
            cost = Barriers.t.Right(y, x).GetComponent<Advance>().cost + 1;
            yp = y;
            xp = x + 1;
        }
        if (Barriers.t.Top(y, x) != null && ((Barriers.t.Top(y, x).GetComponent<Advance>().visited && !Dest) || (Barriers.t.Top(y, x).GetComponent<Advance>().visited2 && Dest)) && Barriers.t.Top(y, x).GetComponent<Advance>().cost != -1 && 
            (Barriers.t.Top(y, x).GetComponent<Advance>().cost < cost - 1 || cost == -1))
        {
            cost = Barriers.t.Top(y, x).GetComponent<Advance>().cost + 1;
            yp = y + 1;
            xp = x;
        }
        if (Barriers.t.Bottum(y, x) != null && ((Barriers.t.Bottum(y, x).GetComponent<Advance>().visited && !Dest) || (Barriers.t.Bottum(y, x).GetComponent<Advance>().visited2 && Dest)) && Barriers.t.Bottum(y, x).GetComponent<Advance>().cost != -1 && 
            (Barriers.t.Bottum(y, x).GetComponent<Advance>().cost < cost - 1 || cost == -1))
        {
            cost = Barriers.t.Bottum(y, x).GetComponent<Advance>().cost + 1;
            yp = y - 1;
            xp = x;
        }
        Checked = false;
    }
    public void Adv()
    {
        if (transform.GetComponent<Image>().color != black && transform.GetComponent<Image>().color != red && transform.GetComponent<Image>().color != green)
        {
            Barriers.t.GetImage(y, x).color = Color.Lerp(Barriers.t.GetImage(y, x).color, darkGreen, 45f * Time.deltaTime);
            if(Barriers.t.GetImage(y, x).color == darkGreen)
            {
                if (Dest)
                {
                    visited2 = true;
                    visited = false;
                }
                else
                {
                    visited = true;
                    visited2 = false;
                }
            }
        }
        else
        {
            start = false;
        }
    }
    void TurnYellow(Image img)
    {
        if (img.GetComponent<Advance>().yp != -1 && img.GetComponent<Advance>().xp != -1)
        {
            img.color = yellow;
            TurnYellow(Barriers.t.GetImage(img.GetComponent<Advance>().yp, img.GetComponent<Advance>().xp));
        }
    }
}