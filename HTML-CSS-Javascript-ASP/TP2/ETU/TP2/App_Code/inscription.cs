using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Classe qui gère une inscription, qu'elle soit pour un match ou pour une conférence
/// </summary>
public class Inscription
{
    private int id = 0;
    private string game = "";
    private int startTime = 0;
    private int endTime = 0;
    private string local = "";
    private bool isConference = false;

	public Inscription(int id, string game, int starttime, int endtime, string local, bool isConference = false)
	{
        this.id = id;
        this.game = game;
        this.startTime = starttime;
        this.endTime = endtime;
        this.local = local;
        this.isConference = isConference;
	}

    public int GetId()
    {
        return this.id;
    }

    public string GetGame()
    {
        return this.game;
    }

    public int GetStartTime()
    {
        return this.startTime;
    }

    public int GetEndTime()
    {
        return this.endTime;
    }

    public string GetLocal()
    {
        return this.local;
    }

    public bool IsConference()
    {
        return this.isConference;
    }
}