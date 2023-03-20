namespace CardboardBox.Database.Postgres;

using BuilderAction = Func<NpgsqlDataSourceBuilder, Task>;
using ConnectAction = Func<NpgsqlConnection, Task>;

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

	/// <summary>
	/// Configuration actions used to change the behaviour of the underlying <see cref="NpgsqlDataSourceBuilder"/>
	/// </summary>
	/// <param name="action">The configuration action</param>
	/// <returns>The current builder for chaining</returns>
	IConnectionInitBuilder OnCreate(BuilderAction action);
}

/// <summary>
/// Exposes the internal collections of the <see cref="IConnectionInitBuilder"/>
/// </summary>
public interface IConnectionInitProvider : IConnectionInitBuilder
{
	/// <summary>
	/// Actions that change the behaviour of the underlying <see cref="NpgsqlDataSourceBuilder"/>
	/// </summary>
	BuilderAction[] Builder { get; }

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
	private readonly List<BuilderAction> _builder = new();
	private readonly List<ConnectAction> _connect = new();

	/// <summary>
	/// Actions that change the behaviour of the underlying <see cref="NpgsqlDataSourceBuilder"/>
	/// </summary>
	public BuilderAction[] Builder => _builder.ToArray();

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

	/// <summary>
	/// Configuration actions used to change the behaviour of the underlying <see cref="NpgsqlDataSourceBuilder"/>
	/// </summary>
	/// <param name="action">The configuration action</param>
	/// <returns>The current builder for chaining</returns>
	public IConnectionInitBuilder OnCreate(BuilderAction action)
	{
		_builder.Add(action);
		return this;
	}
}
