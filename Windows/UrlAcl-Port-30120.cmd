echo off
echo Open Port for everyone
netsh http add urlacl url=http://+:30120/ user=Jeder
echo done
pause
