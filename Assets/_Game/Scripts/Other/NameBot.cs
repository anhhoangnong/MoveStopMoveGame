using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NameBot
{
    private static List<string> names = new List<string>()
    {
        "Hoàng Anh",
        "Kiên",
        "Minh",
        "Bảo",
        "Hoàng",
        "Thuận",
        "Hà",
        "Hùng",
        "Phú",
        "Dũng",
        "Quang",
        "Tuấn",
        "Nam",
        "Đăng",
        "Sơn",
    };

    public static List<string> GetNames(int amount)
    {
        //dùng Guid để random
        var list = names.OrderBy(x => System.Guid.NewGuid());
        return list.Take(amount).ToList();
    }

    public static string GetRandomName()
    {
        return names[Random.Range(0, names.Count)];
    }
}
