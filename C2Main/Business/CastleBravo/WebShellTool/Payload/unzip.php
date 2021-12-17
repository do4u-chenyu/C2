<?php
$file_path = $argv[1];
echo $file_path;
$code=file_get_contents($file_path);
file_put_contents("unzip_result.txt",gzinflate($code));
?>