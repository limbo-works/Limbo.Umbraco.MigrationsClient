using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.MigrationsClient.Models.Skybrud.Grid;

public class GridControl : LegacyObjectBase {

    [JsonIgnore]
    public GridDataModel Model => Section.Model;

    [JsonIgnore]
    public GridSection Section => Row.Section;

    [JsonIgnore]
    public GridRow Row => Area.Row;

    [JsonIgnore]
    public GridArea Area { get; }

    public JToken Value { get; internal set; }

    public GridEditor Editor { get; internal set; }

    [JsonIgnore]
    public GridControl? PreviousControl { get; internal set; }

    [JsonIgnore]
    public GridControl? NextControl { get; internal set; }

    internal GridControl(JObject json, JToken value, GridArea area, GridEditor editor) : base(json) {
        Value = value;
        Area = area;
        Editor = editor;
    }

    public GridControl(GridControl control) : base(control.JObject) {
        Area = control.Area;
        Value = control.Value;
        Editor = control.Editor;
        PreviousControl = control.PreviousControl;
        NextControl = control.NextControl;
        Value = control.Value;
        Editor = control.Editor;
    }

    public bool GetBoolean(string propertyName) {
        return (Value as JObject).GetBoolean(propertyName);
    }

    public string? GetString(string propertyName) {
        return (Value as JObject).GetString(propertyName);
    }

    public T? GetString<T>(string propertyName, Func<string, T> callback) {
        return (Value as JObject).GetString(propertyName, callback);
    }

    public JArray? GetArray(string propertyName) {
        return (Value as JObject).GetArray(propertyName);
    }

    public JObject? GetObject(string propertyName) {
        return (Value as JObject).GetObject(propertyName);
    }

    public TResult? GetObject<TResult>(string propertyName, Func<JObject, TResult> callback) {
        return (Value as JObject).GetObject(propertyName) is {} json ? callback(json) : default;
    }

}

public class GridControl<TValue> : GridControl {

    public new TValue Value { get; }

    public GridControl(GridControl control, TValue value) : base(control) {
        Value = value;
    }

}

public class GridControl<TValue, TConfig> : GridControl<TValue> {

    public new GridEditor<TConfig> Editor { get; }

    public GridControl(GridControl control, TValue value, GridEditor<TConfig> editor) : base(control, value) {
        Editor = editor;
    }

}