namespace CardboardBox.Database.Sqlite;


using ConnectAction = Func<SqliteConnection, Task>;

/// <summary>
/// Represents a builder for changing how connections and data source builders work for Npgsql
/// </summary>
public interface IConnectionInitBuilder
{
	/// <summary>
	/// Action that is executed every time a new SQL connection is opened.
	/// </summary>
	/// <param name="action">The action to perform</param>
	/// <returns>The current builder for chaining</returns>
	IConnectionInitBuilder OnConnect(ConnectAction action);
}

/// <summary>
/// Exposes the internal collections of the <see cref="IConnectionInitBuilder"/>
/// </summary>
public interface IConnectionInitProvider : IConnectionInitBuilder
{
	/// <summary>
	/// Actions that are executed every time a new SQL connection is opened.
	/// </summary>
	ConnectAction[] Connect { get; }
}

/// <summary>
/// A builder for changing how connections and data source builders work for Npgsql
/// </summary>
public class ConnectionInitBuilder : IConnectionInitProvider
{
	private readonly List<ConnectAction> _connect = new();

	/// <summary>
	/// Actions that are executed every time a new SQL connection is opened.
	/// </summary>
	public ConnectAction[] Connect => _connect.ToArray();

	/// <summary>
	/// Action that is executed every time a new SQL connection is opened.
	/// </summary>
	/// <param name="action">The action to perform</param>
	/// <returns>The current builder for chaining</returns>
	public IConnectionInitBuilder OnConnect(ConnectAction action)
	{
		_connect.Add(action);
		return this;
	}
}
