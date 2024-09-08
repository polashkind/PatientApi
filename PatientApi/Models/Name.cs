using System;
using System.Collections.Generic;
using System.Linq;

public class Name
{
    public string Use { get; set; }
    public string? Family { get; set; }
    public string Given { get; set; } = string.Empty;

    public List<string> GivenNames
    {
        get => Given.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        set => Given = string.Join(",", value);
    }
}
