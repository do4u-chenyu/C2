<?php
$input_file = $argv[1];
$output_file = $argv[2];
$content = file_get_contents($input_file);
$zip_content = gzdeflate($content,9);
file_put_contents($output_file, $zip_content);
?>