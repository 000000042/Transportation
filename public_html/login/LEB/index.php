<?php

if (user_logged_in()) {
  get_result();
}else{
  show_login();
}

function get_result()
{
  ?>
  <!DOCTYPE html>
  <html lang="en" dir="ltr">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="IE=edge">
    <meta name="viewport" content="width=device-widgh, intial-scale=1.0">
    <title>صفحه کاربری</title>
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <link rel="stylesheet" href="css/style.css">
    <link rel="shortcut icon" href="img/favicon.png">
    <script src="js/java.js"></script>
    <script src="header.php" session_start();="session_start();"></script>
  </head>
  <body>
    <!--list of btn-->
    <div class="wrapper side-panel-open">
      <div class="main">
        <iframe class="leb-admin" src="https://docs.google.com/spreadsheets/d/e/2PACX-1vQoPIIcDiXJT77sE6cYB8QZn7XBjKqrhss-1GNG_zubZuSwNTdDTeGCzKCpHTLBeOxK9i0XdBRvWOow/pubhtml?gid=988455449&single=true"></iframe>
      </div>
      <button class="side-panel-toggle" type="button">
        <span class="material-icons sp-icon-open">keyboard_double_arrow_left</span>
        <span class="material-icons sp-icon-close">keyboard_double_arrow_right</span>
      </button>
      <div class="side-panel">
        <a class="button" href="http://localhost/sajadtransport/usersection/" style="color:white;">لیست اعلام بار</a>
        <div class="space"></div>
        <a class="button" href="../UDRL/" style="color:white;">لیست رانندگان</a>
        <div class="space"></div>
        <a class="button" href="https://docs.google.com/forms/d/e/1FAIpQLSdCdmLQLKt1aUetRcUFndy3SvB6m0-Cwqma99G5A0MDH1e2zQ/viewform" style="color:white;">ثبت اعلام بار</a>
        <div class="space"></div>
        <a class="button" href="https://docs.google.com/forms/d/e/1FAIpQLSeYx_XlwA19vI-LCK03pk8Z1XEAmY-zVrBC2FUPMigwiICbIg/viewform" style="color:white;">رزور رانندگان</a>
        <div class="space"></div>
      </div>
    </div>
    <script>
      document.querySelector(".side-panel-toggle").addEventListener("click", () => {
        document.querySelector(".wrapper").classList.toggle("side-panel-open");
      });
    </script>
  </body>
  <footer>
    <div class="footer">
      <p><img src="http://localhost/sajadtransport/usersection/img/favicon.png" alt=""></p>
    </div>
  </footer>
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
}
