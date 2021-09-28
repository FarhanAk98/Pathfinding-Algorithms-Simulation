using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dijkstras : MonoBehaviour
{
    Color black, red, green;
    void Start()
    {
        black = new Color32(0, 0, 0, 255);
        red = new Color32(255, 0, 0, 255);
        green = new Color32(0, 255, 0, 255);
    }
    public void Adv2(int y, int x)
    {
        Image img = Barriers.t.GetImage(y, x);
        if (Barriers.t.Left(y, x) != null && Barriers.t.Left(y, x).color != black && !Barriers.t.Left(y, x).GetComponent<Advance>().visited && 
            !Barriers.t.Left(y, x).GetComponent<Advance>().visited2)
        {
            if (img.GetComponent<Advance>().Dest)
            {
                Barriers.t.Left(y, x).GetComponent<Advance>().Dest = true;
            }
            Barriers.t.Left(y, x).GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
            Barriers.t.Left(y, x).GetComponent<Advance>().yp = y;
            Barriers.t.Left(y, x).GetComponent<Advance>().xp = x;
            Barriers.t.Left(y, x).GetComponent<Advance>().start = true;
        }
        if (Barriers.t.Right(y, x) != null && Barriers.t.Right(y, x).color != black && !Barriers.t.Right(y, x).GetComponent<Advance>().visited &&
            !Barriers.t.Right(y, x).GetComponent<Advance>().visited2)
        {
            if (img.GetComponent<Advance>().Dest)
            {
                Barriers.t.Right(y, x).GetComponent<Advance>().Dest = true;
            }
            Barriers.t.Right(y, x).GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
            Barriers.t.Right(y, x).GetComponent<Advance>().yp = y;
            Barriers.t.Right(y, x).GetComponent<Advance>().xp = x;
            Barriers.t.Right(y, x).GetComponent<Advance>().start = true;
        }
        if (Barriers.t.Top(y, x) != null && Barriers.t.Top(y, x).color != black && !Barriers.t.Top(y, x).GetComponent<Advance>().visited &&
            !Barriers.t.Top(y, x).GetComponent<Advance>().visited2)
        {
            if (img.GetComponent<Advance>().Dest)
            {
                Barriers.t.Top(y, x).GetComponent<Advance>().Dest = true;
            }
            Barriers.t.Top(y, x).GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
            Barriers.t.Top(y, x).GetComponent<Advance>().yp = y;
            Barriers.t.Top(y, x).GetComponent<Advance>().xp = x;
            Barriers.t.Top(y, x).GetComponent<Advance>().start = true;
        }
        if (Barriers.t.Bottum(y, x) != null && Barriers.t.Bottum(y, x).color != black && !Barriers.t.Bottum(y, x).GetComponent<Advance>().visited &&
            !Barriers.t.Bottum(y, x).GetComponent<Advance>().visited2)
        {
            if (img.GetComponent<Advance>().Dest)
            {
                Barriers.t.Bottum(y, x).GetComponent<Advance>().Dest = true;
            }
            Barriers.t.Bottum(y, x).GetComponent<Advance>().cost = Barriers.t.GetImage(y, x).GetComponent<Advance>().cost + 1;
            Barriers.t.Bottum(y, x).GetComponent<Advance>().yp = y;
            Barriers.t.Bottum(y, x).GetComponent<Advance>().xp = x;
            Barriers.t.Bottum(y, x).GetComponent<Advance>().start = true;
        }
        img.GetComponent<Advance>().start = false;
    }
}