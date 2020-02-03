using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonInfo
{
    public enum ESex
    {
        Male,
        Female,
        Unknown
    }

    public string Name { get; }
    public DateTime Birthday { get; }

    public ESex Sex { get; }

    public int Age
    {
        get
        {
            int age = DateTime.Today.Year - Birthday.Year;
            if (Birthday > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }


    public PersonInfo(string name, DateTime? birthday = null, ESex sex = ESex.Unknown)
    {
        Name = name;
        Birthday = birthday ?? DateTime.Today;
        Sex = sex;
    }
}