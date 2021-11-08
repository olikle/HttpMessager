Für die Ip oder den Rechnernamen muss der Port in der Firewall freigeschalten werden.

netsh http add urlacl url=http://+:8888/ user=Everyone

Die SDDL konnte nicht erstellt werden. Fehler: 1789:
netsh http add urlacl url=http://+:8888/ user=Jeder

netsh http delete urlacl url=http://+:8888/

netsh http add urlacl url=http://+:8888/ user=Everyone


HOME / WINDOWS COMMANDS / HOW TO ADD A TCP FIREWALL RULE IN WINDOWS USING THE COMMAND PROMPT USING NETSH
How To Add A TCP Firewall Rule In Windows Using The Command Prompt Using NETSH
8532 views Less than a minute
There are many different types of firewall rules which can be controlled using “NETSH ADVFIREWALL FIREWALL”.  It replaced the old “netsh firewall” which was available in Windows XP and earlier.

To add a firewall rule in Windows using the command line to open a specific incoming port enter the following command:

[17:13] Weichselbaumer, Martin
# nicht nötig.
#netsh advfirewall firewall add rule name= "RingPi" dir=in action=allow protocol=TCP localport=30120
#netsh advfirewall firewall delete rule name= "RingPi" dir=in action=allow protocol=TCP localport=8355

netsh http add urlacl url=http://+:30120/ user=Jeder
