# PCShutdown

A program for computer power management (Shutdown, Reboot, Sleep mode, Hibernation, Screen Lock) via a local network.

`curl http://remote_pc_ip:8888?action={reboot|shutdown|lock|sleep|hibernate|cancel}&password=you_password&delay=delay_in_seconds`

for reboot pc via 15 seconds with ip 192.168.0.15 use next command:

`curl http://192.168.0.15:8888?action=reboot&password=you_password&delay=15`

There is also a mini interface available at `http://pc_ip_address:8888`
