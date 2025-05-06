namespace BURN_SOCIETY.Models;

public class KeysSecret
{
    public string consumer_key { get; set; }
    public string consumer_secret { get; set; }
    public KeysSecret(string consumer_key, string consumer_secret)
    {
        this.consumer_key = consumer_key;
        this.consumer_secret = consumer_secret;
    }
}
