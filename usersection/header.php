<?php

session_start();
if (match_found_in_database()) {
    $_SESSION['loggedin'] = true;
    $_SESSION['username'] = $username; // $username coming from the form, such as $_POST['username']
                                       // something like this is optional, of course
}
if (isset($_SESSION['loggedin']) && $_SESSION['loggedin'] == true) {
    echo "Welcome to the member's area, " . $_SESSION['username'] . "!";
} else {
    echo "Please log in first to see this page.";
}