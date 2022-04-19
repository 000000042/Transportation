<?php

if (user_logged_in()) {
    get_result();
}else{
    show_login();
}
function get_result()
{
    ?>

        <html xmlns="http://www.w3.org/1999/xhtml">
        <META charset="utf-8">
        <meta http-equiv="x-ua-compatible" content="IE=edge">
        <meta name="viewport" content="width=device-widgh, intial-scale=1.0">

        <head>
            <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
            <title>صفحه امنیتی شرکت سجاد ساری</title>
            <script type="text/javascript">
                var s = "";

                while (s != "mypassword") {
                    s = prompt("گذرواژه اختصاصی شرکت  سجاد را وارد کنید");
                    if (s == "mypassword") {
                        window.location.href = "http://localhost/sajadtransport/public_html/login/usersection/index.php"; //page to redirect if password entered is correct

                    } else {
                        alert("گذرواژه شما اشتباه میباشد - شما به پل ارتباطی منتقل میشوید");
                        window.location.href = "http://localhost/sajadtransport/landingpagge/";


                    }
                }
            </script>
        </head>

        <body>
        </body>

        </html>
    <?php
}
function show_login()
{
  echo "<pre style='text-align: center; direction: ltr; border:1px solid #1e45cf; padding: 1rem; color: #1e45cf;'>". print_r("YOU ARE NOT LOGGED IN",1) ."</pre>";
}
function user_logged_in()
{
  session_start();
  return isset($_SESSION["loggedinuser"]);
