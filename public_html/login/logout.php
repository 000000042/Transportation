<?php
session_start();
unset($_SESSION["loggedinuser"]);
session_destroy();
