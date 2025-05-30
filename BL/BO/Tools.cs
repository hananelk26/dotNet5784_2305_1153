﻿

using DalApi;
using System.Reflection;

namespace BO;

public static class Tools
{
    public static string ToStringProperty<T>(this T obj)
    {
        // Get the type of the object
        Type type = obj.GetType();

        // Get all public instance properties of the object's type
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        // Initialize the result string
        string result = type.Name + " {" + Environment.NewLine;

        // Iterate through each property
        foreach (var property in properties)
        {
            result += "  " + property.Name + ": "; // Add property name

            // Get the value of the property
            object value = property.GetValue(obj);

            // Check if the value is a list of TaskInList objects
            if (value is List<TaskInList> collection)
            {
                // If it is, add each item in the collection to the result
                result += "[" + Environment.NewLine;
                foreach (var item in collection)
                {
                    result += "    " + item + "," + Environment.NewLine;
                }
                result += "  ]" + Environment.NewLine;
            }
            else
            {
                // If not, add the value directly to the result
                result += value + "," + Environment.NewLine;
            }
        }

        // Add closing bracket to indicate end of object representation
        result += "}" + Environment.NewLine;

        // Return the final string representation
        return result;
    }



}
