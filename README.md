# Nethermind Prune Starter

This is a simple program that can be used to start Nethermind's full pruning process.
It is designed to run within Nethermind's Docker container alongside the client.


## Usage

Start Nethermind with the **publish** contents of this package mounted to it in a volume, e.g. `/setup/NethermindPruneStarter`, and the following arguments (the port is arbitrary, as long as it isn't already in use by something):

```
--Pruning.Mode Full --JsonRpc.Enabled true --JsonRpc.AdditionalRpcUrls \"http://localhost:7434|http|admin\""
```

If you already use `JsonRpc.AdditionalRpcUrls` (e.g. for Merge support), remove the above instance of it and replace your existing one with e.g.:

```
--JsonRpc.AdditionalRpcUrls [\"<your other AdditionalRpcUrl string>\",\"http://localhost:7434|http|admin\"]
```

Once it's up, you can trigger a prune from outside the container like this:

`docker exec -it <container name> docker /setup/NethermindPruneStarter http://127.0.0.1:7434`

This will cause the app to send a request to Nethermind's `admin_prune` route, which will start a full prune, without needing the `admin` to be exposed outside of the container.