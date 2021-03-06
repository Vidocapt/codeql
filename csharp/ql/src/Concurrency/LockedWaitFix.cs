class Message
{
    readonly Object countLock = new Object();
    readonly Object textLock = new Object();

    int count;
    string text;

    public void Print()
    {
        lock (textLock)
        {
            while (text == null)
                System.Threading.Monitor.Wait(textLock);
            lock (countLock)
                System.Console.Out.WriteLine(text + "=" + count);
        }
    }

    public string Text
    {
        set
        {
            lock (countLock)
            {
                lock (textLock)
                {
                    ++count;
                    text = value;
                    System.Threading.Monitor.Pulse(textLock);
                }
            }
        }
    }
}
