<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        @charset "utf-8";
        /* CSS Document */
        body {
            margin: 0;
            padding: 0;
            font-family: 'Open Sans', sans-serif;
            font-size: 12px;
            background: #f1f1f1;
            overflow-x: hidden;
            -webkit-text-stroke: 0px !important;
        }

        input::-webkit-input-placeholder {
            color: #d2d0d0;
        }

        .fare {
            display: none;
        }

        .frc-b {
            width: 100%;
            position: fixed;
            background: rgba(0,0,0,.5);
            height: 100%;
            left: 0;
            right: 0;
            top: 0;
            z-index: 1;
        }

        .frmm {
            width: 100%;
            float: left;
            background: #eaeaea;
            padding: 20px;
        }

        .frzz {
            width: 96%;
            position: fixed;
            left: 0;
            right: 0;
            margin: auto;
            top: 8%;
            z-index: 1;
        }

        .clze {
            position: absolute;
            right: 19px;
            top: 5px;
            font-size: 20px;
            color: #ffffff;
            background: #FF0000;
            width: 27px;
            height: 27px;
            border-radius: 50%;
            text-align: center;
            cursor: pointer;
            font-weight: bold;
        }

        .tr-m1 {
            width: 100%;
            float: left;
        }

        .tr-m {
            width: 100%;
            float: left;
        }

        .trv-main {
            width: 100%;
            float: left;
            box-shadow: 1px 0 8px -2px;
            height: 65px;
            background: #ffffff;
        }

        .tr-cen {
            width: 90%;
            margin: 0 auto;
            max-width: 1200px;
        }

        .mar10 {
            margin-top: 10px;
        }

        .mar15 {
            margin-top: 6px;
        }

        .mar20 {
            margin-top: 8px;
        }

        .fz1 {
            width: 20%;
            float: left;
        }

        .fz1m {
            width: 100%;
        }

        .fmz1 {
            width: 20%;
            float: left;
        }

        .fmz2 {
            width: 70%;
            float: left;
        }

        .fz2 {
            width: 80%;
            float: left;
        }

        .fz2n {
            width: 60%;
            float: left;
        }

        .tmz {
            width: 100%;
            float: left;
        }

        .tmz1 {
            width: 12%;
            float: left;
        }

        .fzz1 {
            width: 100%;
            font-size: 20px;
            font-weight: 600;
        }

        .fzz2 {
            width: 100%;
            font-size: 13px;
            color: #555555;
        }

        .tmz2 {
            width: 11%;
            float: left;
            margin-top: 10px;
        }

        .tmz3 {
            width: 12%;
            float: left;
        }

        .tmz4 {
            width: 12%;
            float: left;
            margin-left: 9%;
            margin-top: 10px;
        }

        .tmz5 {
            width: 6%;
            float: left;
        }

        .sez {
            width: 100%;
            float: left;
            font-size: 12px;
            font-weight: 600;
            margin-bottom: 5px;
        }

        .tazm {
            width: 100%;
            padding-top: 2%;
            float: left;
            margin-top: 0;
        }

        .tazf {
            font-size: 14px;
            float: left;
            font-weight: bold;
            margin-top: 2px;
        }

        .tazr {
            width: 8px;
            background: url(https://b2b.easemytrip.com/Content/img/Currency/inr.png);
            height: 12px;
            float: left;
            display: block;
            margin: 5px 6px 0 9px;
            padding: 0 0;
        }

        .tzm {
            width: 16%;
            font-size: 17px;
            color: #F00;
            float: left;
            font-weight: 600;
        }

        .nzf {
            width: 100%;
            float: left;
            font-size: 20px;
            font-weight: 600;
        }

        .nzf1 {
            width: 100%;
            float: left;
            font-size: 13px;
        }

        .bzm {
            width: 100%;
            float: left;
            padding-left: 11%;
        }

        .bzmm span:nth-child(2) {
            width: 21%;
            float: left;
        }

        .bzmm span:nth-child(3) {
            text-align: right;
        }

        .bzmm span {
            width: 39%;
            float: left;
        }

        .srz {
            float: left;
            font-size: 16px;
            font-weight: 700;
        }

        .szi {
            float: left;
            width: 21px;
            height: 11px;
            background: url(/Content/img/nar.png) no-repeat;
            margin: 5px 5%;
        }

        .szi1 {
            float: left;
            width: 21px;
            height: 11px;
            background: url(/Content/img/w-nar.png);
            margin: 5px 0px;
        }

        .flzc1 {
            width: 100%;
            float: left;
        }

        .flz1 {
            width: 100%;
            float: left;
        }

        .airl {
            width: 58px;
            height: 52px;
            float: left;
        }

        .oth1 {
            width: 100%;
            float: left;
            cursor: pointer;
            margin-top: 10px;
        }

        .trz {
            height: 30px;
            line-height: 30px;
            float: right;
            width: 15%;
            text-align: center;
            background: #ffffff;
            color: #ef6614;
            border: 1px solid #ef6614;
            font-size: 13px;
            border-radius: 4px;
            cursor: pointer;
            outline: 0;
        }

            .trz:hover {
                background: #da5200;
                color: #ffffff;
            }

        .taz1z {
            width: 80%;
            float: left;
        }

        .taz2z {
            width: 20%;
            float: left;
            margin-top: 15px;
        }

            .taz2z a {
                width: 80%;
                text-decoration: none;
                line-height: 40px;
                background-color: #ef6614;
                border-radius: 3px;
                color: #fff;
                text-align: center;
                height: 40px;
                font-size: 19px;
                margin: 2% 0;
                cursor: pointer;
                float: right;
                border: 1px solid #ef6614;
                outline: 0;
            }

                .taz2z a:hover {
                    background: #da5200;
                }

        .fsmz {
            width: 100%;
            float: left;
            margin-top: 10px;
        }

            .fsmz table {
                width: 100%;
                float: left;
                border: 1px solid #dedede;
                font-size: 12px;
                background: #ffffff;
            }

                .fsmz table th {
                    height: 35px;
                    background: #8f8c8c;
                    color: #ffffff;
                    text-align: center;
                }

                .fsmz table td {
                }

        .tbzz {
            width: 100%;
            float: left;
            background: #ffffff;
            margin-bottom: 15px;
            padding: 10px;
            margin-top: 20px;
        }

            .tbzz ul {
                margin: 0;
                padding: 0;
            }

                .tbzz ul li {
                    list-style-type: none;
                    margin-left: 2%;
                    cursor: pointer;
                    background: #eaeaea;
                    float: left;
                    padding: 3px 0 3px 0;
                    width: 10%;
                    cursor: pointer;
                    border-radius: 20px;
                    display: block;
                }

                    .tbzz ul li:nth-child(2) {
                        margin-left: 2%;
                    }

        .cz {
            width: 100%;
            font-size: 13px;
            color: #9b9b9b;
            font-weight: 600;
        }
        /**--Radio--*/
        .radio1 + .radio1, .checkbox + .checkbox {
            margin-top: -5px;
        }

        .radio1, .checkbox {
            position: relative;
            display: block;
        }

            .radio1 input[type="radio"] {
                opacity: 0;
                z-index: 1;
                cursor: pointer;
            }

            .radio1 label {
                display: inline-block;
                vertical-align: middle;
                position: relative;
                padding-left: 5px;
            }

            .radio1 label, .checkbox label {
                min-height: 20px;
                padding-left: 20px;
                margin-bottom: 0;
                font-weight: normal;
                cursor: pointer;
            }

            .radio1 label {
                width: 100%;
                display: inline-block;
                vertical-align: middle;
                position: relative;
                padding-left: 5px;
            }

            .radio1 input:focus ~ label, .radio1 input:valid ~ label {
                font-size: 12px;
                top: 5px;
                color: #4a4949 !important;
            }

            .radio1 label::before {
                content: "";
                display: inline-block;
                position: absolute;
                width: 17px;
                height: 17px;
                left: 0;
                margin-left: 8px;
                border: 2px solid #3f88bc;
                border-radius: 50%;
                background-color: #fff;
                -webkit-transition: border 0.15s ease-in-out;
                -o-transition: border 0.15s ease-in-out;
                transition: border 0.15s ease-in-out;
            }

            .radio1 label::after {
                display: inline-block;
                position: absolute;
                content: " ";
                width: 7px;
                height: 7px;
                left: 13px;
                top: 5px;
                border-radius: 50%;
                background-color: #555555;
                -webkit-transform: scale(0, 0);
                -ms-transform: scale(0, 0);
                -o-transform: scale(0, 0);
                transform: scale(0, 0);
                -webkit-transition: -webkit-transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
                -moz-transition: -moz-transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
                -o-transition: -o-transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
                transition: transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
            }

            .radio1 input[type="radio"] {
                opacity: 0;
                z-index: 1;
                cursor: pointer;
                display: none;
            }

                .radio1 input[type="radio"]:focus + label::before {
                    outline: thin dotted;
                    outline: 5px auto -webkit-focus-ring-color;
                    outline-offset: -2px;
                }

                .radio1 input[type="radio"]:checked + label::after {
                    -webkit-transform: scale(1, 1);
                    -ms-transform: scale(1, 1);
                    -o-transform: scale(1, 1);
                    transform: scale(1, 1);
                }

                .radio1 input[type="radio"]:disabled + label {
                    opacity: 0.65;
                }

            .radio1.radio-inline {
                margin-top: 0;
            }

        .radio-danger1 input[type="radio"] + label::after {
            background-color: #3f88bc;
        }

        .radio-danger1 input[type="radio"]:checked + label::before {
            border-color: #3f88bc;
            outline: 0;
        }

        .radio-danger1 input[type="radio"]:checked + label::after {
            background-color: #3f88bc;
        }
        /**--Radio--*/
        .mar30 {
            margin-top: 30px;
        }

        .mar40 {
            margin-top: 40px;
        }

        .m-bt {
            margin-bottom: 20px;
        }

        .mg1 {
            margin-bottom: 10px;
        }

        .mgin {
            margin-bottom: 10px;
        }

        .mrg1 {
            margin-top: 10px;
        }
        /*.blc_brd23-b2b-new {
    position: fixed;
    left: 0;
    -webkit-filter: blur(5px);
    -moz-filter: blur(5px);
    -o-filter: blur(5px);
    -ms-filter: blur(5px);
    filter: blur(5px);
    top: 0;
    background-color: #000;
    width: 100%;
    height: 100%;
    opacity: 0.7;
    cursor: pointer;
}*/
        .mrg2 {
            margin-top: 20px;
        }

        .clr {
            clear: both;
        }

        #popup {
            position: absolute;
            top: 15%;
            width: 100%;
            display: none;
        }

        .trv-hd {
            width: 100%;
            float: left;
        }

        .trv-l {
            width: 20%;
            float: left;
        }

            .trv-l img {
                padding: 5px 0;
            }

        .trv-c {
            width: 60%;
            float: left;
            text-align: center;
            font-size: 24px;
            color: #1a1a1a;
            line-height: 65px;
        }

        .trv-c1 {
            width: 60%;
            float: left;
            text-align: center;
            font-size: 24px;
            color: #1a1a1a;
            line-height: 65px;
            display: none;
        }

        .trv-c2 {
            width: 60%;
            float: left;
            text-align: center;
            font-size: 24px;
            color: #1a1a1a;
            line-height: 65px;
            display: none;
        }

        .trv-r {
            width: 20%;
            float: left;
        }

        .support {
            width: 100%;
            float: left;
            color: #262626;
            font-size: 13px;
            text-align: right;
            margin-top: 10px;
        }

            .support span {
                width: auto;
                float: right;
            }

                .support span:before {
                    float: left;
                    content: " ";
                    background: url(/Content/img/traveller/img-sprite.png);
                    width: 24px;
                    height: 20px;
                    background-position: -8px -8px;
                }

        .support1 {
            width: 100%;
            float: left;
            color: #262626;
            font-size: 13px;
            text-align: right;
            margin-top: 4px;
        }

            .support1 span {
                width: auto;
                float: right;
            }

                .support1 span a {
                    color: #262626;
                    font-size: 13px;
                    text-decoration: none;
                }

                    .support1 span a:hover {
                        text-decoration: underline;
                    }

                .support1 span:before {
                    float: left;
                    content: " ";
                    background: url(/Content/img/traveller/img-sprite.png);
                    width: 22px;
                    height: 20px;
                    background-position: -37px -8px;
                }

        .rev-m {
            width: 100%;
            float: left;
        }

            .rev-m ol {
                margin: 0;
                padding: 0;
            }

                .rev-m ol li {
                    display: inline;
                    font-size: 13px;
                    margin-right: 1%;
                    float: left;
                    line-height: 30px;
                }

        .ac {
            color: #055c9b;
        }

        .arr {
            width: 16px;
            height: 30px;
            background: url(/Content/img/traveller/li-arr.png) no-repeat;
            display: block;
            background-size: 8px 15px;
            margin-top: 9px;
        }

        .fd-pr {
            width: 100%;
            float: left;
        }

        .fd-ll {
            width: 70.5%;
            float: left;
        }

        .fd-l {
            width: 100%;
            float: left;
        }

        .fd-l1 {
            width: 100%;
            float: left;
            display: none;
        }

        .fd-l2 {
            width: 100%;
            float: left;
            display: none;
        }

        .fd-l3 {
            width: 100%;
            float: left;
            display: none;
        }

        .fd-h {
            width: 100%;
            float: left;
            padding-left: 2%;
            font-size: 18px;
            color: #1a1a1a;
            height: 50px;
            line-height: 50px;
            background: #f8f6f6;
            border-top-left-radius: 4px;
            border-top-right-radius: 4px;
        }

            .fd-h span {
                margin-left: 1%;
            }

            .fd-h:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 35px;
                height: 35px;
                background-position: -60px -4px;
                margin-top: 8px;
            }

        .fd-des {
            width: 100%;
            float: left;
            font-size: 15px;
            color: #1a1a1a;
        }

            .fd-des:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 42px;
                height: 20px;
                background-position: -100px -6px;
            }

            .fd-des span:nth-child(1) {
                font-size: 20px;
                color: #1a1a1a;
                padding-left: 1%;
            }

            .fd-des span:nth-child(2) {
                font-size: 14px;
                color: #6a6868;
            }

        .fd-des1 {
            width: 100%;
            float: left;
            font-size: 15px;
            color: #1a1a1a;
        }

            .fd-des1:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 42px;
                height: 20px;
                background-position: -93px -46px;
            }

            .fd-des1 span:nth-child(1) {
                font-size: 20px;
                color: #1a1a1a;
                padding-left: 1%;
            }

            .fd-des1 span:nth-child(2) {
                font-size: 14px;
                color: #6a6868;
            }

        .fli-d-m {
            width: 100%;
            float: left;
            margin-top: 10px;
        }

        .fli-d-m1 {
            width: 100%;
            float: left;
            margin-top: 5px;
        }

        .fli-d-l {
            width: 20%;
            float: left;
        }

        .fli-d-r {
            width: 80%;
            float: left;
        }

        .tr-cn {
            width: 96%;
            margin: 0 auto;
        }

        .tr-cn-m {
            width: 100%;
            float: left;
        }

        .fli1 {
            width: 20%;
            float: left;
        }

        .fli1-m {
            width: 100%;
            float: left;
            margin-top: 10px;
            s;
        }

        .fli1-m-l {
            width: 20%;
            float: left;
        }

        .fli1-m-r {
            width: 64%;
            float: left;
            margin-left: 8%;
        }

            .fli1-m-r span:nth-child(1) {
                display: block;
                color: #1a1a1a;
                font-size: 16px;
            }

            .fli1-m-r span:nth-child(2) {
                display: block;
                color: #6a6868;
                font-size: 12px;
            }

        .fli2 {
            width: 33%;
            float: left;
        }

        .fli-cm {
            width: 80%;
            float: left;
            font-size: 24px;
            color: #1a1a1a;
            text-align: left;
            padding-left: 20%;
        }

        .lin1 {
            width: 90%;
            height: 1px;
            border-bottom: 1px dotted #b0aeae;
            float: right;
        }

            .lin1:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 10px;
                height: 10px;
                background-position: -290px -58px;
                margin-top: -3px;
            }

        .air-dt {
            width: 80%;
            float: left;
            color: #6a6868;
            font-size: 12px;
            text-align: left;
            margin-top: 5px;
            padding-left: 20%;
        }

            .air-dt span {
                display: block;
            }

        .fli3 {
            width: 33%;
            float: left;
        }

        .stp {
            width: 100%;
            float: left;
            color: #6a6868;
            font-size: 12px;
            text-align: center;
        }

        .lin2 {
            width: 100%;
            height: 3px;
            border-bottom: 1px dotted #b0aeae;
            float: left;
            position: relative;
            margin-top: 15px;
        }

        .fli-i {
            width: 32px;
            height: 32px;
            background: url(/Content/img/traveller/img-sprite.png);
            position: absolute;
            margin: auto;
            left: 0;
            right: 0;
            top: -12px;
            background-position: -265px -3px;
        }

        .fli4 {
            width: 34%;
            float: left;
        }

        .fli-cm1 {
            width: 100%;
            float: left;
            font-size: 24px;
            color: #1a1a1a;
            text-align: left;
            padding-left: 25%;
        }

        .air-dt1 {
            width: 75%;
            float: left;
            color: #6a6868;
            font-size: 12px;
            text-align: left;
            margin-top: 5px;
            padding-left: 25%;
        }

            .air-dt1 span {
                display: block;
            }

        .fd-r {
            width: 100%;
            float: right;
            position: relative;
        }

        .bor {
            float: left;
            width: 100%;
            border: 1px solid #d2d2d2;
            background: #ffffff;
            border-radius: 4px;
            border-bottom: 3px solid #d2d2d2;
        }

        .lin3 {
            width: 90%;
            height: 1px;
            border-bottom: 1px dotted #b0aeae;
            float: left;
        }

            .lin3:after {
                float: right;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 10px;
                height: 10px;
                background-position: -287px -58px;
                margin-top: -3px;
            }

        .ref {
            float: left;
            width: 100%;
            text-align: center;
        }

            .ref span {
                width: 50%;
                display: block;
                font-size: 11px;
                padding: 3px 0;
                color: #2dca1c;
                border-radius: 23px;
                margin: 28px auto;
                border: 1px solid #2dca1c;
                text-align: center;
            }

        .ref-n {
            float: left;
            width: 100%;
            text-align: center;
        }

            .ref-n span {
                width: 80px;
                display: block;
                background: #fd4a4a;
                font-size: 10px;
                color: #ffffff;
                margin: 20px auto;
                height: 15px;
                line-height: 15px;
                text-align: center;
            }

        .prc-mm {
            width: 100%;
            float: left;
            background: #f8f6f6;
            border-top-left-radius: 4px;
            border-top-right-radius: 4px;
        }

        .prc-h {
            width: 63%;
            padding-left: 3%;
            float: left;
            font-size: 18px;
            color: #1a1a1a;
            height: 50px;
            line-height: 50px;
        }

            .prc-h span {
                padding-left: 3%;
                font-size: 18px;
                color: #1a1a1a;
            }

            .prc-h:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 35px;
                height: 35px;
                background-position: -227px -1px;
                margin-top: 7px;
            }

        .prc-h2 {
            width: 13%;
            padding-left: 3%;
            float: left;
            font-size: 13px;
            color: #1a1a1a;
            height: 50px;
            line-height: 50px;
        }

            .prc-h2:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 8px;
                height: 20px;
                background-position: -5px -48px;
                margin-top: 16px;
                padding-right: 10%;
            }

        .prc-h3 {
            width: 9%;
            float: left;
            font-size: 13px;
            color: #1a1a1a;
            height: 50px;
            line-height: 50px;
        }

            .prc-h3:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 10px;
                height: 20px;
                background-position: -21px -49px;
                margin-top: 16px;
                padding-right: 10%;
            }

        .prc-h4 {
            width: 9%;
            float: left;
            font-size: 13px;
            color: #1a1a1a;
            height: 50px;
            line-height: 50px;
        }

            .prc-h4:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 13px;
                height: 20px;
                background-position: -42px -51px;
                margin-top: 16px;
                padding-right: 10%;
            }

        .prc-h1 {
            width: 100%;
            padding-left: 3%;
            float: left;
            font-size: 18px;
            color: #1a1a1a;
            height: 50px;
            line-height: 50px;
            background: #f8f6f6;
            border-top-left-radius: 4px;
            border-top-right-radius: 4px;
        }

            .prc-h1 span {
                color: #adacac;
                font-size: 14px;
            }

        .prm {
            width: 100%;
            float: left;
        }

        .pr {
            width: 100%;
            float: left;
            border-bottom: 1px solid #e5e3e3;
        }

            .pr:last-child {
                border-bottom: 0px;
            }

        .pr-l {
            width: 52%;
            cursor: pointer;
            padding-left: 4%;
            float: left;
            color: #1a1a1a;
            font-size: 13px;
            height: 40px;
            line-height: 40px;
        }

        .pr-l-n {
            width: 52%;
            padding-left: 4%;
            float: left;
            color: #1a1a1a;
            font-size: 13px;
            height: 40px;
            line-height: 40px;
            cursor: pointer;
        }

        .pr-l-nn {
            width: 52%;
            padding-left: 4%;
            float: left;
            color: #1a1a1a;
            font-size: 13px;
            height: 40px;
            line-height: 40px;
            display: none;
            cursor: pointer;
        }

        .pr-r1 {
            width: 46%;
            padding-right: 4%;
            float: left;
            color: #d63b05;
            font-size: 18px;
            text-align: right;
            height: 40px;
            line-height: 40px;
            font-weight: bold;
        }

        .pr-dd {
            width: 100%;
            float: left;
            display: none;
        }

        .curpoint {
            cursor: pointer;
        }

        .pr-d {
            width: 100%;
            float: left;
            border-bottom: 1px solid #e5e3e3;
        }

        .pr-dl {
            width: 47%;
            padding-left: 4%;
            float: left;
            color: #1a1a1a;
            font-size: 13px;
            height: 40px;
            line-height: 40px;
        }

        .pr-dr {
            width: 46%;
            padding-right: 4%;
            float: left;
        }

            .pr-dr span {
                float: right;
                color: #1a1a1a;
                font-size: 13px;
                height: 40px;
                line-height: 40px;
                text-align: right;
                font-weight: bold;
            }

                .pr-dr span:before {
                    float: left;
                    content: " ";
                    background: url(/Content/img/traveller/img-sprite.png);
                    width: 11px;
                    height: 15px;
                    background-position: -63px -50px;
                    margin-top: 13px;
                }

        .pr-r1 {
            width: 46%;
            padding-right: 4%;
            float: left;
            color: #d63b05;
            font-size: 18px;
            text-align: right;
            height: 40px;
            line-height: 40px;
            font-weight: bold;
        }

            .pr-r1 span {
                float: right;
                color: #d63b05;
                font-size: 18px;
                text-align: right;
                height: 40px;
                line-height: 40px;
                font-weight: bold;
            }

                .pr-r1 span:before {
                    float: left;
                    content: " ";
                    background: url(/Content/img/traveller/img-sprite.png);
                    width: 11px;
                    height: 15px;
                    background-position: -78px -49px;
                    margin-top: 13px;
                }

        .pr-l1 {
            width: 52%;
            padding-left: 4%;
            float: left;
            color: #d63b05;
            font-size: 15px;
            height: 40px;
            line-height: 40px;
            font-weight: bold;
        }

        .pr-r {
            width: 48%;
            padding-right: 2%;
            float: right;
        }

            .pr-r img {
                float: right;
            }

            .pr-r span {
                float: right;
                color: #1a1a1a;
                font-size: 13px;
                text-align: right;
                height: 40px;
                line-height: 40px;
                font-weight: bold;
                margin-left: 3%;
            }

        .cpn {
            width: 94%;
            float: left;
            margin: 20px 3% 0;
        }

        .cpn-l {
            width: 70%;
            float: left;
        }

        .cpn input {
            width: 93%;
            float: left;
            padding-left: 4%;
            border: 0px;
            border-bottom: 1px solid #cccccc;
            height: 30px;
            line-height: 30px;
            outline: 0;
        }

        .cpn-r {
            width: 30%;
            float: left;
        }

        .apl {
            width: 100%;
            height: 30px;
            line-height: 30px;
            background: #b5b6b6;
            color: #ffffff;
            font-size: 13px;
            border-radius: 4px;
            text-align: center;
            cursor: pointer;
        }

            .apl:hover {
                background: #a2a3a3;
            }

        .bor p {
            float: left;
            font-size: 10px;
            color: #8b8a8a;
            width: 94%;
            margin: 10px 3% 0;
            padding-bottom: 15px;
        }

        .ad-m {
            width: 100%;
            float: left;
            margin-top: 10px;
        }

        .ml-h {
            width: 100%;
            float: left;
        }

        .ml-hh {
            width: 100%;
            float: left;
            margin-bottom: 7px;
        }

        .ml-h1 {
            width: 80%;
            float: left;
        }

        .tem {
            width: 100%;
            font-size: 15px;
            color: #000000;
            display: block;
        }

        .ml-h1 span {
            padding-left: 2%;
        }

            .ml-h1 span:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 30px;
                height: 36px;
                background-position: -106px -238px;
            }

        .chp {
            width: auto;
            font-size: 12px;
            color: #847f7f;
            padding-left: 6%;
            margin-top: 1px;
        }

        .ml-h2 {
            width: 20%;
            float: left;
        }

        .dwn-ar {
            width: 16px;
            height: 10px;
            float: right;
            background: url(/Content/img/traveller/img-sprite.png);
            background-position: -231px -177px;
            cursor: pointer;
            margin-top: 8px;
        }

        .up-ar {
            width: 10px;
            height: 16px;
            float: right;
            background: url(/Content/img/traveller/img-sprite.png);
            background-position: -250px -170px;
            cursor: pointer;
            display: none;
            margin-top: 8px;
        }

        .ml-hh-n {
            float: left;
            width: 100%;
            margin-bottom: 5px;
        }

        .ml-h1-n {
            width: 80%;
            float: left;
        }

        .tem-n {
            width: 100%;
            font-size: 15px;
            color: #000000;
            display: block;
        }

        .ml-h1-n span {
            padding-left: 0%;
            line-height: 28px;
        }

            .ml-h1-n span:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 32px;
                height: 28px;
                background-position: -221px -242px;
            }

        .chp-n {
            width: auto;
            font-size: 12px;
            color: #847f7f;
            padding-left: 6%;
            margin-top: 4px;
        }

        .ml-h2-n {
            width: 20%;
            float: left;
        }

        .dwn-ar-n {
            width: 16px;
            height: 10px;
            float: right;
            background: url(/Content/img/traveller/img-sprite.png);
            background-position: -231px -177px;
            cursor: pointer;
            display: none;
            margin-top: 5px;
        }

        .up-ar-n {
            width: 10px;
            height: 16px;
            float: right;
            background: url(/Content/img/traveller/img-sprite.png);
            background-position: -250px -170px;
            cursor: pointer;
            margin-top: 3px;
        }

        .mel {
            font-size: 15px;
            color: #000000;
            float: left;
        }

        .mel-d {
            width: 100%;
            float: left;
        }

        .mel1-d {
            width: 100%;
            float: left;
            display: none;
        }

        .mel-dd {
            width: 100%;
            float: left;
            margin-top: 10px;
        }

        .mel-dd1 {
            width: 70%;
            float: left;
            font-size: 14px;
            color: #1a1a1a;
        }

            .mel-dd1 span {
                padding-left: 1%;
            }

            .mel-dd1:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 23px;
                height: 14px;
                background-position: -179px -253px;
                font-size: 14px;
            }

        .mel-dd2 {
            width: 30%;
            float: left;
            font-size: 14px;
            color: #1a1a1a;
            text-align: right;
        }

            .mel-dd2 span {
                width: auto;
                float: right;
                color: #5aad17;
                font-size: 14px;
            }

                .mel-dd2 span:before {
                    float: left;
                    content: " ";
                    background: url(/Content/img/traveller/img-sprite.png);
                    width: 25px;
                    height: 21px;
                    background-position: -145px -249px;
                    font-size: 14px;
                }

        .food-m {
            width: 100%;
            float: left;
            margin: 10px 0 20px 0;
        }

        .food1 {
            width: 31.5%;
            float: left;
            margin-right: 1%;
            border: 1px solid #dcd9d9;
            height: 96px;
            margin-bottom: 1%;
            overflow: hidden;
        }

        .food1-m {
            width: 100%;
            float: left;
        }

        .food1-m1 {
            width: 48%;
            float: left;
        }

        .food1-m2 {
            width: 52%;
            float: left;
        }

        .food2 {
            width: 47.5%;
            float: left;
            margin-left: 2%;
            border: 1px solid #dcd9d9;
            height: 96px;
        }
        /*.me-d{ width:100%; float:left; font-size:13px; color:#969595; margin-top:15px; text-align:center;}*/
        .me-d {
            width: 93%;
            float: right;
            font-size: 11px;
            color: #969595;
            padding: 5px 5px 0 5px;
            text-align: center;
            height: 36px;
            -webkit-line-clamp: 2;
            -webkit-box-orient: vertical;
            overflow: hidden;
            text-overflow: ellipsis;
            display: -webkit-box;
        }

        .food1-m1 img {
            width: 100%;
        }

        .me-rs {
            width: 100%;
            float: left;
            font-size: 18px;
            color: #000000;
            margin-top: 0px;
        }

        .me-rs1 {
            width: 40%;
            float: left;
        }

        .me-rs-i {
            float: right;
            background: url(/Content/img/traveller/img-sprite.png);
            width: 10px;
            height: 15px;
            background-position: -207px -250px;
            font-size: 18px;
            color: #1a1a1a;
            margin-top: 2px;
        }

        .me-rs2 {
            width: 60%;
            float: left;
            font-size: 18px;
            color: #1a1a1a;
        }

        .bag-m {
            width: 100%;
            float: left;
            margin-bottom: 10px;
        }

        .bag1 {
            width: 30%;
            float: left;
            border: 1px solid #dcd9d9;
            margin-right: 1.3%;
            padding: 0 0 9px 0;
            margin-bottom: 1%;
        }

        .bag2 {
            width: 23%;
            float: left;
            border: 1px solid #dcd9d9;
            margin-right: 2.3%;
        }

        .bag3 {
            width: 23%;
            float: left;
            border: 1px solid #dcd9d9;
        }

        .bag4 {
            width: 23%;
            float: right;
            border: 1px solid #dcd9d9;
            margin-left: 2%;
        }

        .ad-bg {
            width: 100%;
            height: auto;
            line-height: 30px;
            padding: 0 4px;
            color: #484747;
            margin: 0 0 8px 0;
            font-size: 14px;
            background: #f2f0f0;
            text-align: center;
        }

        .s-mel-n {
            width: 100%;
            float: left;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .s-mel-n1 {
            width: 30%;
            float: left;
            text-align: right;
            padding-right: 2%;
        }

            .s-mel-n1 input {
                margin: 0;
                padding: 0;
            }

        .s-mel-n2 {
            width: 68%;
            float: left;
        }

        .mel-ddn {
            width: 100%;
            float: left;
            margin: 10px 0 10px;
        }

        .mel-ddn1 {
            width: 70%;
            float: left;
            font-size: 14px;
            color: #1a1a1a;
        }

            .mel-ddn1 span {
                padding-left: 1%;
            }

            .mel-ddn1:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 23px;
                height: 14px;
                background-position: -179px -253px;
                font-size: 14px;
            }

        .mel-ddn2 {
            width: 30%;
            float: left;
            font-size: 14px;
            color: #1a1a1a;
            text-align: right;
        }

            .mel-ddn2 span {
                width: auto;
                float: right;
                color: #5aad17;
                font-size: 14px;
            }

                .mel-ddn2 span:before {
                    float: left;
                    content: " ";
                    background: url(/Content/img/traveller/img-sprite.png);
                    width: 25px;
                    height: 21px;
                    background-position: -145px -249px;
                    font-size: 14px;
                }

        .s-mel {
            width: 100%;
            float: left;
            margin-top: 10px;
        }

        .s-mel1 {
            width: 38%;
            float: left;
            text-align: right;
            padding-right: 2%;
        }

            .s-mel1 input {
                margin: 0;
                padding: 0;
            }

        .s-mel2 {
            width: 60%;
            float: left;
        }

        .s-mel-c {
            width: 80%;
            margin-top: 6px;
            display: none;
            border-radius: 4px;
            margin: 0 10%;
            float: left;
            border: 1px solid #ccc;
        }

            .s-mel-c input:nth-child(1) {
                border-top-left-radius: 4px;
                border-bottom-left-radius: 4px;
            }

            .s-mel-c input:nth-child(3) {
                border-top-right-radius: 4px;
                border-bottom-right-radius: 4px;
            }

        .s-mel-c1 {
            width: 100%;
            float: left;
            margin-top: 6px;
            display: none;
            margin-bottom: 10px;
        }

        #myform {
            width: 69%;
            float: left;
            margin: 0 15%;
            height: 20px;
            border-radius: 4px;
            background: #ffffff;
            border: 1px solid #ccc;
        }

            #myform input:nth-child(1) {
                border-top-left-radius: 4px;
                border-bottom-left-radius: 4px;
            }

            #myform input:nth-child(3) {
                border-top-right-radius: 4px;
                border-bottom-right-radius: 4px;
            }

        .qty {
            width: 33%;
            height: 18px;
            text-align: center;
            float: left;
            outline: 0;
            border: 0;
        }
        /*input.qtyplus { width: 33.5%; height: 18px; float:right; outline:0;  border-top-right-radius: 4px; border-bottom-right-radius: 4px;}*/

        input.qtyplus {
            width: 33.5%;
            height: 18px;
            float: right;
            outline: 0;
            border-top-right-radius: 4px;
            border-bottom-right-radius: 4px;
            border: 0;
            font-size: 17px;
            line-height: 18px;
        }

        /*input.qtyminus { width: 33.5%; height: 18px; float:left; outline:0; border-top-left-radius: 4px; border-bottom-left-radius: 4px; }*/
        input.qtyminus {
            width: 33.5%;
            height: 18px;
            float: left;
            outline: 0;
            border-top-left-radius: 4px;
            border-bottom-left-radius: 4px;
            border: 0;
            font-size: 20px;
            line-height: 18px;
        }

        .s-mel-c input {
            1px solid #bcbcbc;
        }

        input[type="button" i], input[type="submit" i], input[type="reset" i], input[type="file" i]::-webkit-file-upload-button, button {
            padding: 0;
        }



        .ad1 {
            width: 10%;
            float: left;
        }

        .ad-d {
            width: 100%;
            float: left;
            color: #ffffff;
            background: #86bade;
            font-size: 14px;
            height: 30px;
            line-height: 30px;
            border-radius: 4px;
            text-align: center;
            cursor: pointer;
        }

            .ad-d:hover {
                background: #77afd6;
            }

        .ad-mn {
            width: 100%;
            float: left;
            color: #ffffff;
            background: #86bade;
            font-size: 14px;
            display: none;
            cursor: pointer;
        }

        .ad2 {
            width: 4.5%;
            height: 37px;
            float: left;
            background: url(/Content/img/traveller/img-sprite.png);
            background-position: -145px -4px;
            margin-left: 2%;
        }

        .ad3 {
            width: 4.5%;
            height: 37px;
            float: left;
            background: url(/Content/img/traveller/img-sprite.png);
            background-position: -186px -3px;
            margin-left: 1%;
        }

        .ad4 {
            width: 70%;
            float: left;
            font-size: 13px;
            color: #302f2f;
            line-height: 37px;
            padding-left: 1%;
        }

        .in-ter {
            width: 100%;
            border-radius: 4px;
            float: left;
            background: #ffffff;
            border: 1px solid #d2d2d2;
            margin-top: 10px;
            padding: 15px 0;
            border-bottom: 3px solid #d2d2d2;
        }

        .te1 {
            width: 4%;
            float: left;
            margin-left: 2%;
        }

        .pls {
            width: 29px;
            height: 37px;
            float: left;
            background: url(/Content/img/traveller/img-sprite.png);
            background-position: -229px -48px;
        }

        .te2 {
            width: 3%;
            float: left;
        }

            .te2 input {
                width: 14px;
                height: 14px;
            }

        .te3 {
            width: 90%;
            float: left;
            font-size: 13px;
            color: #302f2f;
            font-family: sans-serif;
            padding: 4px 0 0;
        }

            .te3 span {
                font-size: 11px;
                margin-top: 0;
                display: block;
            }

                .te3 span a {
                    text-decoration: none;
                    color: #0c13a7;
                }

            .te3 label {
                font-weight: 600;
            }

            .te3 span a:hover {
                text-decoration: underline;
            }

        .co-m {
            width: 100%;
            float: left;
        }

        .co-l {
            width: 70.5%;
            float: left;
        }

        .co-l-m {
            width: 100%;
            float: left;
        }

        .co-r {
            width: 25%;
            float: left;
        }

        .con-m {
            width: 47%;
            float: left;
            margin: 15px 0 0;
            margin-bottom: 3px;
            overflow: hidden;
        }

            .con-m input {
                border: 1px solid #dad9d9;
                margin-top: 5px;
                border-radius: 4px;
                outline: 0;
                margin: 0;
                font-size: 14px;
                width: 87%;
            }

        .con-m2 {
            width: 47%;
            float: right;
            margin-bottom: 3px;
            margin-top: 10px;
        }

            .con-m2 input {
                border: 1px solid #dad9d9;
                margin-top: 5px;
                border-radius: 4px;
                outline: 0;
                margin: 0;
                font-size: 14px;
            }

        .con-hd {
            width: 100%;
            float: left;
            font-size: 18px;
            color: #1a1a1a;
            text-align: left;
            margin-bottom: 3px;
        }

            .con-hd span {
                text-align: right;
                font-size: 10px;
                color: #8b8a8a;
                float: right;
            }

        .con-m input:visit {
            outline: 0;
        }

        .hv-ps {
            width: 100%;
            float: left;
            color: #302f2f;
            font-size: 14px;
            margin-top: 7px;
        }

            .hv-ps input {
                border: 1px solid #dad9d9;
                border-radius: 4px;
                outline: 0;
                margin: 0;
                float: left;
            }

            .hv-ps label {
                color: #302f2f;
                font-size: 12px;
                float: left;
                margin-left: 1%;
            }

        .con {
            width: 100%;
            float: left;
            text-align: center;
            padding-bottom: 30px;
            margin-top: 30px;
        }

            .con span {
                width: 30%;
                font-size: 22px;
                height: 45px;
                line-height: 45px;
                border-radius: 4px;
                display: block;
                margin: 0 auto;
                color: #ffffff;
                background: #ee7306;
                cursor: pointer;
            }

                .con span:hover {
                    background: #e06d07;
                }

        .cln_im3 {
            background: #ffffff url(/Content/img/traveller/eml.gif);
            background-position: 14px 14px;
            background-repeat: no-repeat;
        }

        .cln_im1 {
            background: #ffffff url(/Content/img/traveller/phn.png);
            background-position: 10px 9px;
            background-repeat: no-repeat;
        }

        /*--Booking Detail--*/

        .bo-de {
            width: 100%;
            float: left;
            margin-top: 20px;
            display: none;
        }

        .bo-hed {
            width: 95%;
            color: #1a1a1a;
            font-size: 18px;
            padding-left: 5%;
            margin-top: 10px;
        }

        .edt {
            background: url(/Content/img/traveller/img-sprite.png);
            width: 45px;
            height: 30px;
            background-position: -40px -92px;
            left: -12px;
            cursor: pointer;
        }

            .edt:hover {
                background: url(/Content/img/traveller/img-sprite.png);
                width: 45px;
                height: 30px;
                background-position: -8px -163px;
                left: -12px;
                cursor: pointer;
            }

        .edt1 {
            background: url(/Content/img/traveller/img-sprite.png);
            width: 45px;
            height: 30px;
            background-position: -40px -92px;
            left: -12px;
            cursor: pointer;
        }

            .edt1:hover {
                background: url(/Content/img/traveller/img-sprite.png);
                width: 45px;
                height: 30px;
                background-position: -8px -163px;
                left: -12px;
                cursor: pointer;
            }

        .po-re {
            position: relative;
        }

        .po-ab {
            position: absolute;
        }

        .fd-h1 {
            width: 100%;
            float: left;
            padding-left: 2%;
            font-size: 18px;
            color: #1a1a1a;
            height: 50px;
            line-height: 50px;
            background: #f8f6f6;
            border-top-left-radius: 4px;
            border-top-right-radius: 4px;
        }

            .fd-h1 span {
                margin-left: 1%;
            }

            .fd-h1:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 35px;
                height: 35px;
                background-position: -92px -85px;
                margin-top: 8px;
            }

        .tr-c {
            width: 96%;
            float: left;
            margin: 0 2%;
        }

        .ps-ad-m {
            width: 100%;
            float: left;
        }

        .ps-de {
            width: 100%;
            float: left;
            margin-top: 20px;
        }

            .ps-de label {
                width: 100%;
                float: left;
                margin-bottom: 5px;
                font-size: 13px;
                font-weight: bold;
            }

        .in {
            outline: 0;
        }

            .in:focus {
                border-color: #409cf2;
                -webkit-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                -moz-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                outline: none;
            }

        .ps5-gst textarea:focus {
            border-color: #409cf2;
            -webkit-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
            -moz-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
            box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
            outline: none;
        }

        .ps1 {
            width: 12%;
            float: left;
            font-size: 14px;
            color: #1a1a1a;
            height: 44px;
            line-height: 44px;
            font-weight: bold;
            padding-top: 20px;
        }

        .ps2 {
            width: 16%;
            float: left;
        }

            .ps2 select {
                width: 100%;
                font-size: 14px;
                height: 40px;
                border: 1px solid #d7d5d5;
                padding-left: 6%;
            }

            .ps2 input {
                float: left;
                text-align: left;
                height: 40px;
                line-height: 40px;
                border: 1px solid #d7d5d5;
                width: 94%;
                padding-left: 4%;
            }

        .inn {
            float: left;
            background: #ffffff url(/Content/img/traveller/img/arr.png) no-repeat 94% 17px;
            width: 14px;
            height: 8px;
        }

        .ps3 {
            width: 36%;
            float: left;
        }

            .ps3 label {
                padding-left: 14%;
            }

            .ps3 input {
                float: right;
                font-size: 14px;
                text-align: left;
                height: 40px;
                line-height: 40px;
                border: 1px solid #d7d5d5;
                width: 86%;
                padding-left: 4%;
            }

        .ps4 {
            width: 36%;
            float: left;
        }

            .ps4 label {
                padding-left: 14%;
            }

            .ps4 input {
                float: right;
                text-align: left;
                height: 40px;
                line-height: 40px;
                border: 1px solid #d7d5d5;
                width: 86%;
                padding-left: 4%;
            }

        .fl-m-m {
            width: 100%;
            float: left;
            text-align: right;
        }

        .fl-m {
            width: 100%;
            float: left;
            text-align: right;
            margin-bottom: 5px;
        }

        .fl-ml {
            width: 42%;
            float: left;
            text-align: right;
        }

        .fl-mr {
            width: 56%;
            float: right;
            text-align: right;
        }

        .ps {
            float: left;
            font-size: 13px;
            color: #858383;
            margin-top: 10px;
            width: auto;
            cursor: pointer;
            display: none;
        }

        .psn1 {
            float: left;
            font-size: 13px;
            color: #858383;
            margin-top: 10px;
            width: auto;
            cursor: pointer;
        }

        .fl-mm {
            float: right;
            font-size: 13px;
            color: #858383;
            margin-top: 10px;
            width: auto;
            cursor: pointer;
            display: none;
        }

        .fl-ma {
            float: right;
            font-size: 13px;
            color: #858383;
            width: auto;
            cursor: pointer;
            margin-top: 10px;
        }

        .fl-d {
            width: 100%;
            float: left;
            position: relative;
            border: 1px solid #e4e5e5;
            padding: 0px 3% 10px;
            margin-top: 15px;
            margin-bottom: 10px;
            display: none;
        }

            .fl-d:after {
                position: absolute;
                content: " ";
                width: 18px;
                height: 10px;
                background: url(/Content/img/traveller/img-sprite.png);
                background-position: -276px -138px;
                right: 13px;
                top: -9px;
            }

        .fl-d7 {
            width: 100%;
            float: left;
            position: relative;
            border: 1px solid #8e8e8e;
            padding: 10px 1%;
            margin-top: 5px;
            background: #f5ffff;
            margin-bottom: 20px;
            display: none;
        }

            .fl-d7:before {
                position: absolute;
                content: " ";
                width: 0;
                height: 0; /* background:url(/Content/img/traveller/img-sprite.png); */ /* background-position:-276px -138px; */
                left: 13px;
                top: -10px;
                border-left: 10px solid transparent;
                border-right: 10px solid transparent;
                border-bottom: 10px solid #bbbbbb;
            }

        .fl-d1 {
            width: 33%;
            float: left;
        }

            .fl-d1 input {
                float: left;
                text-align: left;
                height: 40px;
                line-height: 40px;
                border: 1px solid #d7d5d5;
                width: 94%;
                padding-left: 4%;
                -webkit-box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
                -moz-box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
                box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
            }

        .fl-d2 {
            width: 33%;
            float: left;
        }

            .fl-d2 label {
                padding-left: 9%;
            }

            .fl-d2 input {
                float: right;
                text-align: left;
                height: 40px;
                line-height: 40px;
                border: 1px solid #d7d5d5;
                width: 86%;
                padding-left: 4%;
                -webkit-box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
                -moz-box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
                box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
            }

        .fl-d3 {
            width: 34%;
            float: left;
        }

            .fl-d3 label {
                padding-left: 9%;
            }

            .fl-d3 select {
                float: right;
                text-align: left;
                height: 40px;
                line-height: 40px;
                border: 1px solid #d7d5d5;
                width: 90%;
                padding-left: 4%;
                -webkit-box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
                -moz-box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
                box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
            }

        .fl-d4 {
            width: 100%;
            float: left;
        }

        .fl-dl {
            width: 35%;
            float: left;
        }

        .fl-dr {
            width: 65%;
            float: left;
        }

        .fl-ma {
            width: 100%;
            float: left;
        }

        .fl-mal {
            width: 48%;
            float: left;
            margin-right: 2%;
        }

        .fl-mar {
            width: 48%;
            float: left;
            margin-left: 2%;
        }

        .dob {
            width: 90%;
            float: left;
            height: 42px;
            border: 1px solid #d7d5d5;
            background: #ffffff;
        }

        .dobn {
            width: 90%;
            float: right;
            height: 27px;
            border: 1px solid #d7d5d5;
            background: #ffffff;
            -webkit-box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
            -moz-box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
            box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
        }

        .dob1 {
            width: 34%;
            float: left;
        }

            .dob1 select {
                width: 90%;
                height: 25px;
                padding-left: 15%;
                border-right: 1px solid #d7d5d5;
            }

        .dob2 {
            width: 33%;
            float: left;
        }

            .dob2 select {
                width: 90%;
                height: 25px;
                padding-left: 0%;
                border-right: 1px solid #d7d5d5;
            }

        .dob3 {
            width: 33%;
            float: left;
        }

            .dob3 select {
                width: 90%;
            }

            .dob3 select {
                width: 100%;
                height: 25px;
                padding-left: 8%;
            }

        .do {
            width: 100%;
            float: left;
            height: 27px;
            border: 1px solid #d7d5d5;
            background: #ffffff;
            -webkit-box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
            -moz-box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
            box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
        }

        .dobnn {
            width: 90%;
            float: right;
            height: 42px;
            border: 1px solid #d7d5d5;
            background: #ffffff;
        }

        .dob1n {
            width: 34%;
            float: left;
        }

            .dob1n select {
                width: 90%;
                height: 25px;
                padding-left: 15%;
                border-right: 1px solid #d7d5d5;
            }

        .dob2n {
            width: 33%;
            float: left;
        }

            .dob2n select {
                width: 90%;
                height: 25px;
                padding-left: 5%;
                border-right: 1px solid #d7d5d5;
            }

        .dob3n {
            width: 33%;
            float: left;
        }

            .dob3n select {
                width: 100%;
                height: 25px;
                padding-left: 8%;
            }

        .fl-d5 {
            width: 100%;
            float: left;
        }

            .fl-d5 label {
                padding-left: 1%;
            }

            .fl-d5 input {
                float: left;
                text-align: left;
                height: 25px;
                line-height: 25px;
                border: 1px solid #d7d5d5;
                width: 100%;
                padding-left: 4%;
                -webkit-box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
                -moz-box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
                box-shadow: 3px 3px 0px 0px rgba(220, 218, 218, 0.5);
            }

        .fl-d6 {
            width: 100%;
            float: left;
        }

            .fl-d6 label {
                display: block;
                margin-bottom: 3px;
                float: left;
                padding-left: 5%;
                color: #0e0e0e;
                font-size: 10px;
                text-transform: uppercase;
                line-height: 27px;
            }

            .fl-d6 input {
                float: right;
                text-align: left;
                height: 40px;
                line-height: 40px;
                border: 1px solid #d7d5d5;
                width: 86%;
                padding-left: 4%;
            }

        .lbl {
            width: 100%;
            float: left;
            color: #0e0e0e;
            line-height: 27px;
            font-size: 11px;
            text-align: left;
            margin-bottom: 3px;
            text-transform: uppercase;
        }

        .ln {
            width: 100%;
            float: left;
            height: 1px;
            background: #f8f6f6;
        }

        .sel {
            float: left;
            background: #ffffff url(/Content/img/traveller/arr.png) no-repeat 94% 9px;
            width: 14px;
            height: 8px;
            border: 0px;
            -webkit-appearance: none !important;
            -moz-appearance: none !important;
            outline: 0;
            font-size: 12px;
            color: #1a1a1a;
        }

        .seln {
            float: left;
            background: #ffffff url(/Content/img/traveller/arrr.png) no-repeat 85% 10px;
            width: 14px;
            height: 8px;
            border: 0px;
            -webkit-appearance: none !important;
            -moz-appearance: none !important;
            outline: 0;
            font-size: 12px;
            color: #1a1a1a;
        }

        .seln2 {
            float: left;
            background: #ffffff url(/Content/img/traveller/arrr.png) no-repeat 85% 10px;
            width: 14px;
            height: 8px;
            border: 0px;
            -webkit-appearance: none !important;
            -moz-appearance: none !important;
            outline: 0;
            font-size: 12px;
            color: #1a1a1a;
        }

        .inf {
            width: 55%;
            float: right;
            margin-top: 15px;
        }

        .sel1 {
            background: #ffffff url(/Content/img/traveller/arr.png) no-repeat 94% 17px;
            width: 14px;
            height: 8px;
            border: 1px solid #d7d5d5;
            -webkit-appearance: none !important;
            -moz-appearance: none !important;
            outline: 0;
            font-size: 13px;
            color: #1a1a1a;
        }

        .inf1 {
            width: 25%;
            float: left;
            padding-left: 5%;
            font-size: 13px;
            color: #000000;
            height: 40px;
            line-height: 40px;
        }

        .inf2 {
            width: 25%;
            float: left;
        }

            .inf2 select {
                width: 90%;
                height: 40px;
                padding-left: 8%;
                float: right;
            }

        .inf3 {
            width: 25%;
            float: left;
        }

            .inf3 select {
                width: 90%;
                height: 40px;
                padding-left: 8%;
                float: right;
            }

        .inf4 {
            width: 25%;
            float: left;
        }

            .inf4 select {
                width: 90%;
                height: 40px;
                padding-left: 8%;
                float: right;
            }

        .dt-ma {
            width: 100%;
            float: left;
            text-align: right;
            font-size: 9px;
            color: #858383;
            margin-top: 5px;
            margin-bottom: 10px;
        }

        .con-m1 {
            width: 60%;
            float: left;
            margin-top: 20px;
        }

        .con-hd1 {
            width: 100%;
            float: left;
            font-size: 18px;
            color: #1a1a1a;
            text-align: left;
        }

        .mo-u {
            float: left;
            margin-top: 1px;
            width: 100%;
            font-size: 10px;
            color: #8b8a8a;
        }

        .inp {
            height: 40px;
            line-height: 40px;
            width: 90%;
            padding-left: 10%;
        }

            .inp:focus {
                border-color: #409cf2;
                -webkit-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                -moz-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                outline: none;
            }

        .hv-ps1 {
            width: 100%;
            float: left;
            color: #302f2f;
            font-size: 12px;
            margin-top: 5px;
        }

            .hv-ps1 input {
                float: left;
                margin: 0;
            }

            .hv-ps1 span {
                float: left;
                margin-left: 1%;
            }

        .inp-m {
            height: 40px;
            line-height: 40px;
            width: 100%;
            padding-left: 14%;
            border: 1px solid #dad9d9;
            border-radius: 4px;
            margin-top: 3px;
            font-size: 16px;
            color: #111;
        }

            .inp-m:focus {
                border-color: #409cf2;
                -webkit-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                -moz-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                outline: none;
            }

        .con1 {
            width: 100%;
            float: left;
            text-align: center;
            padding-bottom: 30px;
            margin-top: 30px;
        }

            .con1 span {
                width: 30%;
                font-size: 22px;
                height: 45px;
                line-height: 45px;
                border-radius: 4px;
                display: block;
                margin: 0 auto;
                color: #ffffff;
                background: #ee7306;
                cursor: pointer;
            }

                .con1 span:hover {
                    background: #e06d07;
                }
        /*--Payment--*/
        .py-de {
            width: 100%;
            float: left;
            margin-top: 20px;
            display: none;
        }

        .ps-d {
            width: 100%;
            float: left;
            margin-bottom: 20px;
        }

        .ps1n {
            width: 27%;
            float: left;
            font-size: 14px;
            word-wrap: break-word;
            padding-right: 1%;
        }

        .fnt {
            float: left;
            color: #000000;
            width: 100%;
        }

        .fnt-g {
            float: left;
            color: #878787;
            width: 100%;
            margin-top: 10px;
        }

        .ps2n {
            width: 20%;
            float: left;
            font-size: 14px;
        }

        .ps3n {
            width: 19%;
            float: left;
            font-size: 14px;
            padding-right: 1%;
        }

        .ps4n {
            width: 19%;
            float: left;
            font-size: 14px;
            padding-right: 1%;
        }

        .ps5n {
            width: 15%;
            float: left;
            font-size: 14px;
        }

        .fd-h2 {
            width: 100%;
            float: left;
            padding-left: 2%;
            font-size: 18px;
            color: #1a1a1a;
            height: 50px;
            line-height: 50px;
            background: #f8f6f6;
            border-top-left-radius: 4px;
            border-top-right-radius: 4px;
        }

            .fd-h2 span {
                margin-left: 1%;
                line-height: 50px;
            }

            .fd-h2:before {
                float: left;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 35px;
                height: 35px;
                background-position: -227px -3px;
                margin-top: 11px;
            }

        .pay-m {
            width: 96%;
            float: left;
            border: 1px solid #e1e0e0;
            margin: 20px 2% 0;
            border-radius: 4px;
            margin-bottom: 20px;
        }

        .pay-l {
            width: 25%;
            float: left;
            -webkit-box-shadow: 4px -1px 6px -4px rgba(0,0,0,0.75);
            -moz-box-shadow: 4px -1px 6px -4px rgba(0,0,0,0.75);
            box-shadow: 4px -1px 6px -4px rgba(0,0,0,0.75);
            min-height: 440px;
        }

        .pay-r {
            width: 68%;
            float: right;
        }

        .pay-mm {
            width: 100%;
            float: left;
            margin: 0 0 3%;
        }

            .pay-mm ul {
                margin: 0 0 0 0;
                padding: 0;
            }

                .pay-mm ul li {
                    font-size: 14px;
                    color: #000000;
                    list-style: none;
                    height: 50px;
                    line-height: 50px;
                    padding: 0 6% 0 4px;
                    border-bottom: 1px solid #d7d5d5;
                }

                    .pay-mm ul li:nth-child(1) {
                        border-top: 1px solid #d7d5d5;
                    }

        .ca-m {
            width: 94%;
            float: left;
            margin: 0 2% 0 4%;
        }

        .car-mm {
            width: 100%;
            float: left;
            margin-top: 10px;
        }

            .car-mm label {
                width: 100%;
                float: left;
                font-size: 13px;
            }

                .car-mm label span {
                    margin-top: 10px;
                    float: left;
                }

            .car-mm input {
                height: 40px;
                line-height: 40px;
                width: 98%;
                border: 1px solid #d7d5d5;
                padding-left: 2%;
                color: #b7b1b1;
                font-size: 14px;
            }

                .car-mm input:focus {
                    border-color: #409cf2;
                    -webkit-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                    -moz-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                    box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                    outline: none;
                }

        .nt-m {
            width: 94%;
            float: left;
            margin: 0 2% 0 4%;
            display: none;
        }

        .nt-h {
            width: 100%;
            float: left;
            color: #69696d;
            font-size: 12px;
            font-weight: bold;
            margin-top: 20px;
        }

            .nt-h span {
                font-weight: normal;
            }

        .bnk {
            width: 100%;
            float: left;
            margin-top: 5px;
        }

            .bnk select {
                height: 40px;
                line-height: 40px;
                width: 98%;
                border: 1px solid #d7d5d5;
                padding-left: 2%;
                color: #b7b1b1;
                font-size: 14px;
                border-radius: 4px;
            }

        .n-re {
            width: 100%;
            float: left;
            margin-top: 10px;
            color: #bebebe;
            font-size: 12px;
        }

        .pr-mm {
            width: 100%;
            float: left;
        }

        .pr-m {
            width: 100%;
            float: left;
            margin-top: 10px;
        }

        .prm-n {
            width: 100%;
            float: left;
        }

            .prm-n span {
                float: left;
                font-size: 14px;
                height: 30px;
                line-height: 30px;
                font-weight:;
                width: 44%;
            }

            .prm-n img {
                float: left;
            }

            .prm-n i {
                float: left;
                margin-left: 2%;
                font-size: 13px;
                height: 30px;
                line-height: 30px;
            }

        .pr-m1 {
            float: left;
            width: auto;
            color: #333333;
            font-size: 14px;
            font-weight: bold;
            padding-top: 10px;
            padding-right: 2%;
        }

            .pr-m1:after {
                float: right;
                content: " ";
                background: url(/Content/img/traveller/img-sprite.png);
                width: 11px;
                height: 15px;
                background-position: -33px -72px;
            }

        .pr-m2 {
            float: left;
            width: auto;
            color: #d63b05;
            font-size: 18px;
            font-weight: bold;
            margin-left: 1%;
        }

        .in-c {
            width: 100%;
            float: left;
            color: #333333;
            font-size: 14px;
            margin-top: 5px;
        }

        .con3 {
            width: 100%;
            float: left;
            text-align: center;
            padding-bottom: 30px;
            margin-top: 30px;
        }

            .con3 span {
                width: 35%;
                font-size: 22px;
                height: 55px;
                line-height: 55px;
                border-radius: 4px;
                display: block;
                margin: 0 auto;
                color: #ffffff;
                background: #ee7306;
                cursor: pointer;
            }

                .con3 span:hover {
                    background: #e06d07;
                }

        .mst {
            margin: 0 auto 20px;
            width: 164px;
            height: 37px;
            background: url(/Content/img/traveller/img-sprite.png);
            background-position: -60px -164px;
        }

        .wl-m {
            width: 94%;
            float: left;
            margin: 0 2% 0 4%;
            display: none;
        }

        .wl {
            width: 100%;
            float: left;
            margin-top: 20px;
        }

        .wl-l {
            width: 30%;
            float: left;
            border: 1px solid #d7d5d5;
            border-radius: 5px;
            height: 50px;
            margin-right: 2%;
        }

            .wl-l:nth-child(4) {
                width: 30%;
                float: left;
                border: 1px solid #d7d5d5;
                border-radius: 5px;
                height: 50px;
                margin-top: 15px;
                margin-right: 2%;
            }

        .wl1 {
            width: 100%;
            float: left;
        }

        .wl2 {
            width: 80%;
            float: left;
        }

        .py {
            float: left;
            margin-left: 30%;
            content: " ";
            width: 95px;
            height: 25px;
            background: url(/Content/img/traveller/img-sprite.png);
            background-position: -9px -206px;
        }

        .wid {
            width: 100%;
        }

        .py1 {
            float: left;
            width: 100%;
        }

            .py1:before {
                float: left;
                margin-left: 30%;
                content: " ";
                width: 95px;
                height: 25px;
                background: url(/Content/img/traveller/img-sprite.png);
                background-position: -107px -206px;
            }

        .py2 {
            float: left;
            width: 100%;
        }

            .py2:before {
                float: left;
                margin-left: 30%;
                content: " ";
                width: 95px;
                height: 22px;
                background: url(/Content/img/traveller/img-sprite.png);
                background-position: -208px -206px;
            }

        .py3 {
            float: left;
            width: 100%;
        }

            .py3:before {
                float: left;
                margin-left: 30%;
                content: " ";
                width: 95px;
                height: 26px;
                background: url(/Content/img/traveller/img-sprite.png);
                background-position: -8px -250px;
            }

        .bsl {
            width: 100%;
            background: #86bade;
            position: relative;
            color: #ffffff !important;
        }
            /*.bsl:after{position:absolute;content:" ";width:10px; height:20px; background:url(/Content/img/traveller/img-sprite.png); background-position:-261px -132px; margin-top:12px; right:-9px;}*/
            .bsl:before {
                float: left;
                content: " ";
                width: 32px;
                height: 25px;
                background: url(/Content/img/traveller/img-sprite.png);
                background-position: -161px -132px;
                margin: 14px 5px 0 0px;
            }

        .bsl1 {
            width: 100%;
            background: #86bade;
            position: relative;
            color: #ffffff !important;
        }
            /*.bsl1:after{position:absolute;content:" ";width:10px; height:20px; background:url(/Content/img/traveller/img-sprite.png); background-position:-261px -132px; margin-top:12px; right:-9px;}*/
            .bsl1:before {
                float: left;
                content: " ";
                width: 32px;
                height: 33px;
                background: url(/Content/img/traveller/img-sprite.png);
                background-position: -224px -125px;
                margin: 7px 5px 0 0px;
            }

        .bsl2 {
            width: 100%;
            background: #86bade;
            position: relative;
            color: #ffffff !important;
        }
            /*.bsl2:after{position:absolute;content:" ";width:10px; height:20px; background:url(/Content/img/traveller/img-sprite.png); background-position:-261px -132px; margin-top:12px; right:-9px;}*/
            .bsl2:before {
                float: left;
                content: " ";
                width: 32px;
                height: 25px;
                background: url(/Content/img/traveller/img-sprite.png);
                background-position: -193px -132px;
                margin: 12px 5px 0 0px;
            }

        .d1 {
            cursor: pointer;
        }

        .d2 {
            cursor: pointer;
        }

        .d3 {
            cursor: pointer;
        }

        .bsadd {
            width: 100%;
            background: #86bade;
            position: relative;
            color: #ffffff !important;
        }
            /*.bsadd:after {
    position: absolute;
    content: " ";
    width: 10px;
    height: 20px;
    background: url(/Content/img/traveller/img-sprite.png);
    background-position: -261px -132px;
    margin-top: 12px;
    right: -9px;
}*/
            .bsadd:before {
                float: left;
                content: " ";
                width: 32px;
                height: 25px;
                background: url(/Content/img/aadmin.png);
                background-position: 2px -1px;
                margin: 14px 7px 0 0px;
            }


        .car-mm1 {
            width: 100%;
            float: left;
            margin-top: 10px;
        }

        .car-mm1n {
            width: 100%;
            float: left;
            margin-top: 10px;
        }

        .car-mm1 label {
            width: 100%;
            float: left;
            font-size: 13px;
            margin-bottom: 5px;
        }

        .car-mm1 input {
            height: 40px;
            line-height: 40px;
            width: 98%;
            border: 1px solid #d7d5d5;
            padding-left: 2%;
            color: #b7b1b1;
            font-size: 14px;
        }

            .car-mm1 input:focus {
                border-color: #409cf2;
                -webkit-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                -moz-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                outline: none;
            }

        .car-mm1n label {
            width: 100%;
            float: left;
            font-size: 13px;
            margin-bottom: 5px;
        }


        .con2 {
            width: 100%;
            float: left;
            text-align: center;
            padding-bottom: 30px;
            margin-top: 30px;
        }

            .con2 span {
                width: 50%;
                font-size: 22px;
                height: 55px;
                line-height: 55px;
                border-radius: 4px;
                display: block;
                margin: 0 auto;
                color: #ffffff;
                background: #ee7306;
                cursor: pointer;
            }

                .con2 span:hover {
                    background: #e06d07;
                }

        .visa:after {
            float: right;
            content: " ";
            background: url(/Content/img/traveller/img-sprite.png);
            width: 150px;
            height: 30px;
            background-position: -136px -92px;
        }

        .cv-m {
            width: 100%;
            float: left;
        }

        .cv1 {
            width: 30%;
            float: left;
        }

            .cv1 select {
                height: 44px;
                line-height: 44px;
                border: 1px solid #d7d5d5;
                width: 90%;
                padding-left: 3%;
            }

        .cv2 {
            width: 30%;
            float: left;
        }

            .cv2 select {
                height: 44px;
                line-height: 44px;
                border: 1px solid #d7d5d5;
                width: 90%;
                padding-left: 3%;
            }

        .cv3 {
            width: 30%;
            float: left;
        }

            .cv3 label {
                display: block;
                height: 14px;
            }

        .crd {
            width: 100%;
            height: 40px;
            line-height: 40px;
            float: left;
            position: relative;
        }

            .crd input {
                height: 40px;
                line-height: 40px;
                width: 98%;
                border: 1px solid #d7d5d5;
                padding-left: 4%;
                color: #b7b1b1;
                font-size: 14px;
                position: relative;
            }

                .crd input:focus {
                    border-color: #409cf2;
                    -webkit-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                    -moz-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                    box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                    outline: none;
                }

        .cr {
            position: absolute;
            right: -2%;
            background: url(/Content/img/traveller/img-sprite.png);
            width: 50px;
            height: 36px;
            background-position: -10px -126px;
            top: 5px;
        }

        .dr {
            float: left;
            width: 32px;
            height: 25px;
            background: url(/Content/img/traveller/img-sprite.png);
            background-position: -63px -133px;
            margin: 14px 5px 0 0px;
            display: none;
        }

        .dr1 {
            float: left;
            width: 32px;
            height: 33px;
            background: url(/Content/img/traveller/img-sprite.png);
            background-position: -95px -125px;
            margin: 7px 5px 0 0px;
        }

        .dr2 {
            float: left;
            width: 32px;
            height: 25px;
            background: url(/Content/img/traveller/img-sprite.png);
            background-position: -125px -130px;
            margin: 12px 5px 0 0px;
        }

        .mn {
            width: 13px;
            height: 13px;
            background: url(img/mn.png) no-repeat;
            float: left;
        }

        .pl {
            width: 13px;
            height: 13px;
            background: url(img/pls.png) no-repeat;
            float: left;
        }

        .con-m1 {
            width: 60%;
            float: left;
            margin-top: 5px;
            display: none;
        }

        .con-m2mm {
            width: 100%;
            float: left;
        }

        .con-m2n {
            width: 50%;
            float: right;
            margin-top: 15px;
        }

        .con-m2nr {
            width: 48%;
            float: right;
            margin-top: 15px;
        }

        .inp {
            height: 40px;
            line-height: 40px;
            width: 90%;
            padding-left: 13%;
        }

        .con-m1 input {
            border: 1px solid #dad9d9;
            margin-top: 5px;
            border-radius: 4px;
            outline: 0;
        }

            .con-m1 input:focus {
                border-color: #409cf2;
                -webkit-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                -moz-box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                box-shadow: 0px 0px 14px 0px rgba(64,156,242,1);
                outline: none;
            }

        .hv-ps2 {
            width: 100%;
            float: left;
            color: #302f2f;
            font-size: 14px;
            margin-top: 5px;
        }

            .hv-ps2 input {
                float: left;
                margin: 0;
            }

            .hv-ps2 span {
                float: left;
                margin-left: 1%;
            }

        .cln_im {
            background: #ffffff url(../img/paaword-icon2.gif);
            background-position: 14px 8px;
            background-repeat: no-repeat;
        }

        .blur {
            filter: blur(2px);
            -webkit-filter: blur(2px);
            -moz-filter: blur(2px);
            -o-filter: blur(2px);
            -ms-filter: blur(2px);
        }

        #overlay {
            position: fixed;
            height: 100%;
            width: 100%;
            display: none;
            left: 0px;
            top: 0px;
            right: 0px;
            bottom: 0px;
            background: rgba(0,0,0,.7);
            z-index: 999;
        }
        /**--Radio--*/
        .radio + .radio, .checkbox + .checkbox {
            margin-top: -5px;
        }

        .radio, .checkbox {
            position: relative;
            display: block;
        }

            .radio input[type="radio"] {
                opacity: 0;
                z-index: 1;
                cursor: pointer;
            }

            .radio label {
                display: inline-block;
                vertical-align: middle;
                position: relative;
                padding-left: 5px;
            }

            .radio label, .checkbox label {
                min-height: 20px;
                padding-left: 20px;
                margin-bottom: 0;
                font-weight: normal;
                cursor: pointer;
            }

            .radio label {
                display: inline-block;
                vertical-align: middle;
                position: relative;
                padding-left: 5px;
            }

            .radio input:focus ~ label, .radio input:valid ~ label {
                font-size: 12px;
                top: -5px;
                color: #4a4949 !important;
            }

            .radio label::before {
                content: "";
                display: inline-block;
                position: absolute;
                width: 17px;
                height: 17px;
                left: 0;
                margin-left: 20px;
                border: 2px solid #1b5fbd;
                border-radius: 50%;
                background-color: #fff;
                -webkit-transition: border 0.15s ease-in-out;
                -o-transition: border 0.15s ease-in-out;
                transition: border 0.15s ease-in-out;
            }

            .radio label::after {
                display: inline-block;
                position: absolute;
                content: " ";
                width: 11px;
                height: 11px;
                left: 25px;
                top: 5px;
                border-radius: 50%;
                background-color: #555555;
                -webkit-transform: scale(0, 0);
                -ms-transform: scale(0, 0);
                -o-transform: scale(0, 0);
                transform: scale(0, 0);
                -webkit-transition: -webkit-transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
                -moz-transition: -moz-transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
                -o-transition: -o-transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
                transition: transform 0.1s cubic-bezier(0.8, -0.33, 0.2, 1.33);
            }

            .radio input[type="radio"] {
                opacity: 0;
                z-index: 1;
                cursor: pointer;
            }

                .radio input[type="radio"]:focus + label::before {
                    outline: thin dotted;
                    outline: 5px auto -webkit-focus-ring-color;
                    outline-offset: -2px;
                }

                .radio input[type="radio"]:checked + label::after {
                    -webkit-transform: scale(1, 1);
                    -ms-transform: scale(1, 1);
                    -o-transform: scale(1, 1);
                    transform: scale(1, 1);
                }

                .radio input[type="radio"]:disabled + label {
                    opacity: 0.65;
                }

            .radio.radio-inline {
                margin-top: 0;
            }


        .radio-danger input[type="radio"] + label::after {
            background-color: #1b5fbd;
        }

        .radio-danger input[type="radio"]:checked + label::before {
            border-color: #1b5fbd;
            outline: 0;
        }

        .radio-danger input[type="radio"]:checked + label::after {
            background-color: #1b5fbd;
        }
        /**--Radio--*/


        /*--Loader--*/
        .container_loader {
            background: rgba(0, 0, 0, 0.5) none repeat scroll 0 0;
            font-family: Helvetica;
            height: 100%;
            margin: 0;
            padding: 0;
            position: fixed;
            width: 100%;
            z-index: 9999;
            display: none;
        }

        .loader {
            height: 20px;
            width: 300px;
            position: absolute;
            top: -40%;
            bottom: 0;
            left: 0;
            right: 0;
            margin: auto;
        }

        .lodng-pg {
            width: 100%;
            background-color: #fff;
            font-family: roboto, tahoma, Arial, Helvetica, sans-serif;
            border-radius: 3px;
        }

        .lodng-pg2 {
            width: 91%;
            margin: 0 auto;
            padding: 5% 4% 7% 4%;
        }

        .pl-wt {
            font-size: 17px;
            font-weight: 500;
            color: #000;
            width: 99%;
            padding: 3% 0%;
        }

        .pl-wt-l {
            width: 17%;
            float: left;
        }

            .pl-wt-l img {
                max-width: 80%;
            }

        .pl-wt-r {
            width: 76%;
            float: left;
            font-size: 12px;
            color: #afafaf;
            margin-left: 7%;
            margin-top: 4%;
        }

        .loader--text {
            color: #fff;
            font-size: 36px;
            left: 0;
            margin: auto;
            position: absolute;
            right: 0;
            text-align: center;
            top: 200%;
            width: 22rem;
        }

        .clr {
            clear: both;
        }

        /*--Loader End--*/


        /* The sticky */
        .sidebar {
            position: -webkit-sticky;
            position: sticky;
            top: 0px;
            width: 25%;
            float: right;
        }

        .in-ter {
            width: 100%;
            border-radius: 4px;
            float: left;
            background: #ffffff;
            border: 1px solid #d2d2d2;
            margin-top: 10px;
            padding: 15px 0;
            border-bottom: 3px solid #d2d2d2;
        }

        .s-inr {
            margin-top: 15px;
        }

        .s-aed {
            margin-top: 16px;
        }

        .t-inr {
            margin-top: 5px;
        }

        .t-aed {
            margin-top: 10px;
        }
        /*--Sticky End--*/

        @media only screen and (min-width:120px) and (max-width:786px) {
            .bsl {
                width: 100%;
            }

            .inp-m {
                padding-left: 13% !important;
            }

            .prm-n span {
                width: 60%;
                font-size: 14px;
            }

            .pay-mm ul {
                margin: 0;
            }

            .mgin {
                margin-bottom: 0px !important;
            }
        }

        .fr_rules_cr2-b2b-new {
            float: right;
            z-index: 999;
            padding: 22px 36px;
            cursor: pointer;
        }


        .bx_f-b2b-new {
            width: 39%;
            margin: 0 28%;
            background: #fff;
            position: fixed;
            z-index: 999999;
            font-size: 14px;
            border-radius: 4px 4px 0 0;
            word-break: break-all;
            padding: 10px; /* border: 1px solid #696969; */
        }

        .new-btn-wid {
            padding: 15px;
        }

        .cntnt_f3-b2b-new {
            padding: 15px;
            float: left;
            width: 100%;
            position: relative;
            background: #fff;
        }

        .fr_rules2-b2b-new {
            background: #f1f1f1;
            font-size: 20px;
            font-weight: bold;
            padding: 20px;
        }

        .new-contin {
            padding: 10px;
            width: 27%;
            background: orange;
            color: #fff;
            border-radius: 4px;
            float: left;
            margin: 0 10px;
            text-align: center;
            cursor: pointer;
        }

        .te3-gst {
            width: 100%;
            float: left;
            cursor: pointer;
            font-size: 14px;
            color: #302f2f;
            padding: 0 15px;
            font-weight: 900;
        }

        no-spin::-webkit-inner-spin-button, .no-spin::-webkit-outer-spin-button {
            -webkit-appearance: none !important;
            -moz-appearance: textfield !important;
            outline: none !important;
        }

        .new-open {
            overflow: inherit !important;
        }

        .ps1-gst {
            width: 24%;
            float: left;
        }



            .ps1-gst label {
                padding-left: 10%;
            }

        .ps2-gst label {
            padding-left: 14%;
        }

        .ps3-gst label {
            padding-left: 14%;
        }

        .ps4-gst label {
            padding-left: 14%;
        }

        .ps5-gst label {
            padding-left: 3%;
        }

        .te3-gst {
            width: 100%;
            float: left;
            cursor: pointer;
            font-size: 14px;
            color: #302f2f;
            padding: 0 15px;
            font-weight: 600;
        }


        .ps1-gst input {
            float: right;
            text-align: left;
            height: 40px;
            line-height: 40px;
            border: 1px solid #d7d5d5;
            width: 90%;
            padding-left: 4%;
        }

        .ps2-gst input {
            float: right;
            text-align: left;
            height: 40px;
            line-height: 40px;
            border: 1px solid #d7d5d5;
            width: 86%;
            padding-left: 4%;
        }

        .ps3-gst input {
            float: right;
            text-align: left;
            height: 40px;
            line-height: 40px;
            border: 1px solid #d7d5d5;
            width: 86%;
            padding-left: 4%;
        }

        .ps4-gst input {
            float: right;
            text-align: left;
            height: 40px;
            line-height: 40px;
            border: 1px solid #d7d5d5;
            width: 86%;
            padding-left: 4%;
        }

        .ps5-gst textarea {
            float: left;
            text-align: left;
            line-height: 24px;
            border: 1px solid #d7d5d5;
            width: 93%;
            padding-left: 1%;
            margin: 2px 3%;
        }

        .opns {
            color: #696969;
            font-size: 11PX !important;
            margin: 2px 6px;
            font-weight: bold;
        }



        .ps2-gst {
            width: 24%;
            float: left;
        }

        .ps3-gst {
            width: 24%;
            float: left;
        }

        .ps4-gst {
            width: 24%;
            float: left;
        }

        .ps5-gst {
            width: 100%;
            float: left;
            margin: 10px 0;
        }

        /*contact*/

        .mb-m {
            width: 100%;
            float: left;
        }

        .mob-l {
            width: 17%;
            float: left;
            margin-right: 2%;
            margin-top: 3px;
        }

        .cod {
            width: 100%;
            float: left;
            height: 40px;
            line-height: 40px;
            font-size: 18px;
            border: 1px solid #dad9d9;
            padding-left: 8%;
            background: #ffffff;
            border-radius: 3px;
        }

            .cod span {
                float: left;
            }


            .cod input {
                width: 53%;
                margin-left: 10%;
                box-shadow: none;
                border: 0px;
                float: left;
                height: 34px;
                border-radius: 3px;
                padding-top: 8%;
            }

        .mob-r {
            width: 79%;
            float: left;
        }
        /*contact*/

        .errorcase {
            background: #a84737;
            border-color: #a84737;
            font-size: 14px;
            color: #fff;
            padding: 10px;
            margin-top: 10px;
            margin-bottom: 0px;
        }

        .erroricon {
            background: url(../Content/img/traveller/error_sprite.png) no-repeat scroll 0 0 rgba(0, 0, 0, 0);
            background-position: 0 -89px;
            height: 21px;
            margin: 0 10px 0 0;
            width: 7%;
        }


        .insurance {
            width: 31%;
            float: left;
            border: 1px solid #dcd9d9;
            margin-right: 2%;
            margin-top: 1%;
            margin-bottom: 1%;
            height: 150px;
        }

            .insurance .me-rs {
                width: 100%;
                float: left;
                font-size: 18px;
                color: #000000;
                margin-top: 5px;
                text-align: center;
            }

            .insurance .me-rs2 {
                width: 100%;
                float: left;
                font-size: 18px;
                color: #1a1a1a;
            }

            .insurance .s-mel-n2 {
                width: 100%;
                float: left;
                text-align: center;
            }

        .adu-tra1 {
            width: 30px;
            margin-right: 4px;
            float: left;
        }

        .adu-tra2 {
            width: auto;
            float: left;
        }


        #divInsuranceTab .ml-h1-n span:before {
            background: none;
        }

        #divInsuranceTab .ml-h1-n {
            width: 97%;
            float: left;
        }

        #divInsuranceTab .ml-h2-n {
            width: auto;
            float: right;
        }

        #divInsuranceTab .te1 {
            width: 5%;
            float: left;
            margin-left: 0px;
            margin-right: 1%;
        }

        .showmore-button {
            cursor: pointer;
            color: #337ab7;
            text-transform: lowercase;
            text-align: right;
            margin-top: 5px;
        }

        /* tab keshav */
        .tab_trvlr {
            overflow: hidden;
            margin: 0 0 15px 0;
            padding: 0;
            background: #fff;
            width: 100%;
            float: left;
            border-bottom: 1px solid #e0e0e0;
        }

            /* Style the buttons inside the tab */
            .tab_trvlr a {
                cursor: pointer;
                float: left;
                padding: 10px;
                list-style: none;
                color: #999898;
                font-size: 13px;
                margin-right: 12px;
                outline: 0;
            }

                /* Change background color of buttons on hover */
                .tab_trvlr a:hover {
                    text-decoration: none;
                }

                /* Create an active/current tablink class */
                .tab_trvlr a.active_tb {
                    border-bottom: 3px solid #3f88bc;
                    background: #fff;
                    color: #3f88bc !important;
                }

        /* Style the tab content */
        .tabc_trvlr {
            display: none;
            padding: 6px 10px;
            border: none;
        }

        .tr-cn-tv {
            width: 100%;
            margin: 0;
        }

        .ml-hh-tv {
            width: 96%;
            margin: 5px auto 12px auto;
            display: table;
        }

        .paxtype {
            font-size: 12px;
            color: #000;
        }

        .paxnumber {
            font-size: 12px;
            color: #000;
        }

        .selectSeatbtn {
            background: #e2e2e2;
            padding: 4px 8px 4px 10px !important;
            margin-top: 8px;
            border: 1px solid #e2e2e2;
            border-radius: 4px;
            margin-left: 2%;
        }

        .changermv {
            font-size: 12px;
            margin-left: 1%;
            cursor: pointer;
        }

        .can-bnew-rg {
            width: 100%;
            float: right;
            margin: 0px;
            padding: 0px;
        }

        .can-b2b-tr {
            width: 100%;
            margin: 0 0;
            padding: 0px;
            float: left;
        }

        .lef-b2b-cane {
            width: 48%;
            padding: 0px;
            margin: 0px;
            float: left;
        }

        .b2b-ca-char {
            width: 100%;
            padding: 0 0 6px;
            font-size: 12px;
            font-weight: 600;
        }

        .b2b-can-tabe {
            margin: 0px;
            padding: 0px;
            font-size: 12px;
            width: 100%;
            float: left;
            background: #fff;
        }

            .b2b-can-tabe td {
                padding: 10px;
            }

        .rig-b2b-cane {
            width: 48%;
            padding: 0px;
            margin: 0px;
            float: right;
        }

        .tr-b2b-cancgr {
            width: 100%;
            float: left;
            margin: 0px 0 10px 0;
            padding: 0 0 0;
        }

        .trm-had {
            margin: 0px;
            padding: 10px 0;
            font-size: 18px;
            font-weight: 600;
        }

        .terms-b2b2-cancahe {
            font-size: 10px;
            color: #000;
            padding: 0;
            height: 67px;
            overflow-x: hidden;
        }

            .terms-b2b2-cancahe ul {
                margin: 0;
                padding-left: 14px;
            }

                .terms-b2b2-cancahe ul li {
                    list-style-type: disc;
                }

        .new-b2b-bagge-main {
            margin: 0 auto 10px auto;
            padding: 0;
            width: 99%;
            float: left;
            border: 1px solid #d9d9d9;
        }

        .bageg-tab-b2b {
            background: #faf9f9;
            border-bottom: 1px solid #d9d9d9;
            float: left;
            width: 100%;
            color: #111;
            font-size: 13px;
            font-family: sans-serif;
        }

        .bagg-b2b-air {
            margin: 0px;
            padding: 9px 11px 0;
            width: 33%;
            float: left;
            text-align: center;
            font-weight: 600;
        }

        .bageg-tab-b2b-new {
            margin: 0 0 0px;
            padding: 0 0 5px 0;
            width: 100%;
            float: left;
            font-size: 13px;
            border-bottom: 1px solid #d9d9d9;
        }

        .check-b2b-air {
            margin: 0px;
            padding: 10px 0;
            width: 29%;
            float: left;
            text-align: center;
            font-weight: 600;
        }

        .cabin-b2b-air {
            margin: 0px;
            padding: 10px 0;
            width: 32%;
            text-align: center;
            float: left;
            font-weight: 600;
        }

        .fli-lo-new-bagge {
            margin: 0px;
            padding: 0px;
            width: 24%;
            float: left;
        }

        .fli-lo-aiame-bagge {
            margin: 0px;
            padding: 0 0 0 4px;
            width: 50%;
            float: left;
        }

        .airline-name {
            font-size: 14px;
            color: #333;
            width: 100%;
            text-align: center;
            float: left;
            font-weight: 600;
        }

        .airline-name-code {
            font-size: 12px;
            color: #959494;
            text-align: center;
            width: 100%;
            float: left;
        }

        #trdate {
            padding: 4px;
            border: 1px solid #bcbcbc;
            border-radius: 4px;
            width: 50%;
            text-align: center;
            outline: none;
        }

        .logo-xs {
            width: 46px;
            margin: 0 auto;
            text-align: center;
            display: table;
        }



        .main_frm_f-b2b-new {
            position: absolute;
            z-index: 99999;
            width: 100%;
            float: left;
            background: #fff;
            padding: 0 0 20px;
            border: 3px solid #111;
        }
    </style>


</head>
<body>
    <div id="divRvSec" class="fd-l3 m-bt" style="display: block;">
        <!--Booking Detail Start-->
        <div class="bor po-re">
            <div class="bo-hed">
                <div class="edt po-ab"></div>
                Booking Details
            </div>
            <div class="tr-cn">
                <!-- ngRepeat: (jID,j) in Jrneys -->
                <div data-ng-repeat="(jID,j) in Jrneys" class="ng-scope">
                    <!-- ngRepeat: (bID,b) in j.segs[0].bonds -->
                    <div class="tr-cn-m ng-scope" data-ng-repeat="(bID,b) in j.segs[0].bonds">
                        <div class="fd-des mar10"><span ng-bind="b.org +' - '+ b.des" class="ng-binding">BOM - DEL</span> | <span ng-bind="b.Legs[0].depDT" class="ng-binding">Fri-07Feb2020</span></div>
                        <!-- ngRepeat: (lID,l) in b.Legs -->
                        <div class="fli-d-m ng-scope" data-ng-repeat="(lID,l) in b.Legs">
                            <div class="fli1">
                                <div class="fli1-m">
                                    <div class="fli1-m-l">
                                        <img src="/Content/AirlineLogo/G8.gif" alt="Flight"></div>
                                    <div class="fli1-m-r"><span ng-bind="l.airName" id="airName" class="ng-binding">G8</span><span ng-bind="l.airCode+'-'+l.fltNum" id="airCode" class="ng-binding">G8- 329</span></div>
                                </div>
                            </div>
                            <div class="fli-d-r">
                                <div class="fli2">
                                    <div class="fli-cm"><span ng-bind="l.org" class="ng-binding">BOM</span> <span><strong ng-bind="l.depTM" class="ng-binding">06:00</strong></span></div>
                                    <div class="lin1"></div>
                                    <div class="air-dt">
                                        <span ng-bind="l.depDT" class="ng-binding">Fri-07Feb2020</span>
                                        <span ng-bind="'Terminal - '+l.depTer" class="ng-binding">Terminal - 1</span>
                                    </div>
                                </div>
                                <div class="fli3">
                                    <div class="stp"><span ng-bind="l.duration" class="ng-binding">02h 10m</span> | <span ng-bind="b.stops" class="ng-binding">Non-Stop</span></div>
                                    <div class="lin2">
                                        <div class="fli-i"></div>
                                    </div>
                                    <div class="clr"></div>
                                    <div class="ref"><span ng-bind="j.segs[0].Fare.lstPxPF[0].Refundable=='True'?'Refundable':'Non-Refundable'" class="ng-binding">Refundable</span></div>
                                </div>
                                <div class="fli4">
                                    <div class="fli-cm1"><span ng-bind="l.dest" class="ng-binding">DEL</span> <span><strong ng-bind="l.arrTM" class="ng-binding">08:10</strong></span></div>
                                    <div class="lin3"></div>
                                    <div class="air-dt1">
                                        <span ng-bind="l.arrDT" class="ng-binding">Fri-07Feb2020</span>
                                        <span ng-bind="'Terminal - '+l.arrTer" class="ng-binding">Terminal - 2</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end ngRepeat: (lID,l) in b.Legs -->
                        <!--fare baggage div-->
                        <div class="mel1-d" style="display: block;">
                            <div class="tab_trvlr">
                                <a class="tabHightLightRv" id="tabFRv00" onclick="tabHightLight(this.id)" ng-click="BindFairDtail_BaggageDetails(j.segs[0].Fare,b,'FareRulesRv'+jID+''+bID,'F');">Fare Rules</a>
                                <a class="tabHightLightRv" id="tabBRv00" onclick="tabHightLight(this.id)" ng-click="BindFairDtail_BaggageDetails(j.segs[0].Fare,b,'BaggageRv'+jID+''+bID,'B');">Baggage</a>
                                <!-- ngIf: IsDomestic==false -->
                                <span ng-bind="j.segs[0].RmkMsg" style="float: right; color: green; font-size: 14px; margin-bottom: -1px;" class="ng-binding">Avail 50% Cancellation fees with Rescheduling Charges Rs.0 and Free Meal</span>
                            </div>
                            <!-- ngIf: 'VisaRulesRv'+jID+''+bID == divToOpen -->
                            <!-- ngIf: 'FareRulesRv'+jID+''+bID == divToOpen -->
                            <!-- ngIf: 'BaggageRv'+jID+''+bID == divToOpen -->
                        </div>
                        <!--fare baggage div end-->
                    </div>
                    <!-- end ngRepeat: (bID,b) in j.segs[0].bonds -->
                </div>
                <!-- end ngRepeat: (jID,j) in Jrneys -->
            </div>
        </div>
        <!--Booking Detail End-->
    </div>
</body>
</html>
