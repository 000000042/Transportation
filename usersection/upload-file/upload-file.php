<?php
// transfer data's into new direc
$targetpath = "./upload-file" . basename($_FILES["inpfile"]["name"]);
move_uploaded_file($files["inpfile"]["tmp_name"], $targetpath);