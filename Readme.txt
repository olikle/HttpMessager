Für die Ip oder den Rechnernamen muss der Port in der Firewall freigeschalten werden.

netsh http add urlacl url=http://+:8888/ user=Everyone

Die SDDL konnte nicht erstellt werden. Fehler: 1789:
netsh http add urlacl url=http://+:8888/ user=Jeder

netsh http delete urlacl url=http://+:8888/

