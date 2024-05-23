using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Community.OpenOrClosed;

public class OpenOrClosedItem {

    public Guid Id { get; }

    public DayOfWeek DayOfTheWeek { get; }

    public bool IsOpen { get; }

    public IReadOnlyList<OpenOrClosedOpenItem> HoursOfBusiness { get; }

    public OpenOrClosedItem(Guid id, DayOfWeek dayOfTheWeek, bool isOpen, IReadOnlyList<OpenOrClosedOpenItem> hoursOfBusiness) {
        Id = id;
        DayOfTheWeek = dayOfTheWeek;
        IsOpen = isOpen;
        HoursOfBusiness = hoursOfBusiness;
    }

    public static OpenOrClosedItem Parse(JObject source) {

        Guid id = source.GetGuid("id");
        DayOfWeek dayOfTheWeek = source.GetEnum<DayOfWeek>("dayoftheweek");
        bool isOpen = source.GetBoolean("isOpen");
        OpenOrClosedOpenItem[] items = source.GetArrayItems("hoursOfBusiness", OpenOrClosedOpenItem.Parse);

        return new OpenOrClosedItem(id, dayOfTheWeek, isOpen, items);

    }

}