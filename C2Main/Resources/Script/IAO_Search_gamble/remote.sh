#! /bin/sh

step=0
while getopts p:s:f:r: OPT; do
	case $OPT in
		s|+s)
		step="$OPTARG"
		;;
		p|+p)
		defaultPort="$OPTARG"
		;;
		f|+f)
		workspace="$OPTARG"
		;;
		r|+r)
		rule="$OPTARG"
		;;
	    *)
		echo "usage: `basename $0` [-s runstep] [-p port] [-f workspace] [-r rulefile]"
		exit 2
	esac
done

getDaemonIps(){
    netstat -ntpa | grep "queryagent" | grep 9871 |awk -F ' ' '{print $5}' | sort | awk -va=: -F ':' '{print $1a}'|awk -vb=$defaultPort '{print $1b}' > valid_ips.txt
    df -h /tmp/iao/search_toolkit | grep "/" | awk -F ' ' '{print $4}' > size.txt
}

judgeConnect(){
    hosts="$hosts `cat select_valid_ips.txt`"
    for host in $hosts; do
        ip=`echo $host   | awk -F':' '{print $1}'`
        port=`echo $host | awk -F':' '{print $2}'`
        if [ "" = "$port" ] ;then
            port=22
        fi
        ssh -p $port -lroot $ip "REMOTEHOST=$ip"
    done
    rm -f size.txt
    rm -f valid_ips.txt
}

runTask(){
    hosts="$hosts `cat select_valid_ips.txt`"
    result_path="results"
    mkdir -p ./$result_path

    for host in $hosts; do
        ip=`echo $host   | awk -F':' '{print $1}'`
        port=`echo $host | awk -F':' '{print $2}'`
        if [ "" = "$port" ] ;then
            port=22
        fi

        ssh -p $port -lroot $ip "REMOTEHOST=$ip; mkdir -p $workspace; head -n1 /home/search/bin/runmaxtimemonitor.py" 2>&1|tee 1>result
        if cat result|grep "Connection refused">/dev/null;then
        echo "$ip connection refused" >> invalidIP
        elif cat result|grep "Connection timed out">/dev/null;then
        echo "$ip connection timed out" >> invalidIP
        elif cat result|grep "No such file or directory">/dev/null;then
        echo "$ip No runmaxtimemonitor.py" >> invalidIP
        else
        echo "do $ip" >>./$result_path/"log.txt"
        scp -P $port ./$rule root@$ip:$workspace >>./$result_path/"log.txt"
        (nohup ssh -p $port -lroot $ip "REMOTEHOST=$ip; cd $workspace; . /home/search/search_profile;  python /home/search/bin/runmaxtimemonitor.py --timeout=28800 python $rule" 2>>./$result_path/"log.txt" | zip ./$result_path/$ip".zip" -) &
        fi
    done

    cat invalidIP
    rm -f result
}

rmDaemonWorkSpace(){
    ps -ef | | grep "python $rule" | grep -v grep| awk -F ' ' '{print $2}' | xargs kill 9
    hosts="$hosts `cat select_valid_ips.txt`"
    for host in $hosts; do
        ip=`echo $host   | awk -F':' '{print $1}'`
        port=`echo $host | awk -F':' '{print $2}'`
        if [ "" = "$port" ] ;then
            port=22
        fi
        ssh -p $port -lroot $ip "REMOTEHOST=$ip; rm -rf $workspace"
    done
}

if [ $step -eq 1 ] ; then
    getDaemonIps
elif [ $step -eq 2 ] ; then
    judgeConnect
elif [ $step -eq 3 ] ; then
    runTask
elif [ $step -eq 4 ] ; then
    rmDaemonWorkSpace
fi
