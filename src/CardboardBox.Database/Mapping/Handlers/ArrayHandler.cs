using System.Text.Json;

namespace CardboardBox.Database.Mapping;

/// <summary>
/// Handles mapping Array Generics from string results.
/// This is a polyfill to add proper Generic Array handling to engines like SQLite or MSSQL
/// </summary>
/// <typeparam name="T">The type of generic array</typeparam>
public class ArrayHandler<T> : SqlMapper.TypeHandler<T[]>
{
	/// <summary>
	/// Parses the input value into a proper generic array
	/// </summary>
	/// <param name="value">The value to parse</param>
	/// <returns>The parsed generic array</returns>
	public override T[] Parse(object value)
	{
		if (value == null || value is not string str) return Array.Empty<T>();

		return JsonSerializer.Deserialize<T[]>(str) ?? Array.Empty<T>();
	}

	/// <summary>
	/// Sets the database parameter value to the proper representation for a generic array
	/// </summary>
	/// <param name="parameter">The database parameter to set</param>
	/// <param name="value">The value to set it to</param>
	public override void SetValue(IDbDataParameter parameter, T[] value)
	{
		value ??= Array.Empty<T>();
		parameter.Value = JsonSerializer.Serialize(value);
	}
}
