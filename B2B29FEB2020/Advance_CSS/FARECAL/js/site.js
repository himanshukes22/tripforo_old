//// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
//// for details on configuring this project to bundle and minify static web assets.

//// Write your Javascript code.



//var jq = $.noConflict(),
//    FormWizard;
//jq(document).ready(function () {
//    FormWizard.init()
//});
//FormWizard = function () {
//    return {
//        init: function () {
//            var u, r;
//            if (jQuery().bootstrapWizard) {
//                var n = jq("#submit_form"),
//                    t = jq(".alert-danger", n),
//                    i = jq(".alert-success", n);
//                n.validate({
//                    doNotHideMessage: !0,
//                    errorElement: "span",
//                    errorClass: "help-block help-block-error",
//                    focusInvalid: !1,
//                    rules: {
//                        username: {
//                            minlength: 5,
//                            required: !0
//                        },
//                        firstname: {
//                            minlength: 1,
//                            required: !0
//                        },
//                        lastname: {
//                            minlength: 2,
//                            required: !0
//                        },
//                        password: {
//                            minlength: 5,
//                            required: !0
//                        },
//                        rpassword: {
//                            minlength: 5,
//                            required: !0,
//                            equalTo: "#submit_form_password"
//                        },
//                        fullname: {
//                            required: !0
//                        },
//                        email: {
//                            required: !0,
//                            email: !0
//                        },
//                        phone: {
//                            required: !0
//                        },
//                        gender: {
//                            required: !0
//                        },
//                        address: {
//                            required: !0
//                        },
//                        city: {
//                            required: !0
//                        },
//                        country: {
//                            required: !0
//                        },
//                        card_name: {
//                            required: !0
//                        },
//                        card_number: {
//                            minlength: 16,
//                            maxlength: 16,
//                            required: !0
//                        },
//                        card_cvc: {
//                            digits: !0,
//                            required: !0,
//                            minlength: 3,
//                            maxlength: 4
//                        },
//                        card_expiry_date: {
//                            required: !0
//                        },
//                        "payment[]": {
//                            required: !0,
//                            minlength: 1
//                        },
//                        "debit[]": {
//                            required: !0,
//                            minlength: 1
//                        },
//                        "credit[]": {
//                            required: !0,
//                            minlength: 1
//                        },
//                        "NetBanking[]": {
//                            required: !0,
//                            minlength: 1
//                        },
//                        "wallets[]": {
//                            required: !0,
//                            minlength: 1
//                        },
//                        "MobilePayments[]": {
//                            required: !0,
//                            minlength: 1
//                        }
//                    },
//                    messages: {
//                        "payment[]": {
//                            required: "Please select at least one option",
//                            minlength: jQuery.validator.format("Please select at least one option")
//                        },
//                        "debit[]": {
//                            required: "Please select your debit card type",
//                            minlength: jQuery.validator.format("Please select your debit card type")
//                        },
//                        "credit[]": {
//                            required: "Please select your credit card type",
//                            minlength: jQuery.validator.format("Please select your credit card type")
//                        },
//                        "NetBanking[]": {
//                            required: "Please select your net banking type",
//                            minlength: jQuery.validator.format("Please select your net banking type")
//                        },
//                        "wallets[]": {
//                            required: "Please select your wallet type",
//                            minlength: jQuery.validator.format("Please select your wallet type")
//                        },
//                        "MobilePayments[]": {
//                            required: "Please select your mobile payments type",
//                            minlength: jQuery.validator.format("Please select your mobile payments type")
//                        }
//                    },
//                    errorPlacement: function (n, t) {
//                        "gender" == t.attr("name") ? n.insertAfter("#form_gender_error") : "payment[]" == t.attr("name") ? n.insertAfter("#form_payment_error") : "debit[]" == t.attr("name") ? n.insertAfter("#form_payment_errordebit") : "credit[]" == t.attr("name") ? n.insertAfter("#form_payment_errorcredit") : n.insertAfter(t)
//                    },
//                    invalidHandler: function () {
//                        i.hide();
//                        t.show();
//                        App.scrollTo(jq(".help-block-error"), -400)
//                    },
//                    highlight: function (n) {
//                        jq(n).closest(".form-group").removeClass("has-success").addClass("has-error")
//                    },
//                    unhighlight: function (n) {
//                        jq(n).closest(".form-group").removeClass("has-error")
//                    },
//                    success: function (n) {
//                        "gender" == n.attr("for") || "payment[]" == n.attr("for") || "debit[]" == n.attr("for") || "credit[]" == n.attr("for") ? (n.closest(".form-group").removeClass("has-error").addClass("has-success"), n.remove()) : n.addClass("valid").closest(".form-group").removeClass("has-error").addClass("has-success")
//                    },
//                    submitHandler: function (n) {
//                        i.show();
//                        t.hide();
//                        n[0].submit()
//                    }
//                });
//                u = function () {
//                    jq("#tab4 .form-control-static", n).each(function () {
//                        var t = jq('[name="' + jq(this).attr("data-display") + '"]', n),
//                            i;
//                        (t.is(":radio") && (t = jq('[name="' + jq(this).attr("data-display") + '"]:checked', n)), t.is(":text") || t.is("textarea")) ? jq(this).html(t.val()) : t.is("select") ? jq(this).html(t.find("option:selected").text()) : t.is(":radio") && t.is(":checked") ? jq(this).html(t.attr("data-title")) : "payment[]" == jq(this).attr("data-display") ? (i = [], jq('[name="payment[]"]:checked', n).each(function () {
//                            i.push(jq(this).attr("data-title"))
//                        }), jq(this).html(i.join("<br>"))) : "debit[]" == jq(this).attr("data-display") ? (i = [], jq('[name="debit[]"]:checked', n).each(function () {
//                            i.push(jq(this).attr("data-title"))
//                        }), jq(this).html(i.join("<br>"))) : "credit[]" == jq(this).attr("data-display") && (i = [], jq('[name="credit[]"]:checked', n).each(function () {
//                            i.push(jq(this).attr("data-title"))
//                        }), jq(this).html(i.join("<br>")))
//                    })
//                };
//                r = function (n, t, i) {
//                    var e = t.find("li").length,
//                        o = i + 1,
//                        u, r, f;
//                    for (jq(".step-title", jq("#form_wizard_1")).text("Step " + (i + 1) + " of " + e), jQuery("li", jq("#form_wizard_1")).removeClass("done"), u = t.find("li"), r = 0; i > r; r++) jQuery(u[r]).addClass("done");
//                    f = jq("#submit_form").serialize();
//                    jq.blockUI();
//                    jq.ajax({
//                        url: UrlBase + "/Hotel/SaveInfoForCheckOut/",
//                        type: "POST",
//                        data: f
//                    }).success(function (n) {
//                        jq.unblockUI();
//                        n != null && n != "" && n.Status == "1" ? window.location.href = UrlBase + "/Hotel/CheckOut?key=" + tjq("#Key").val() + "&refNo=" + tjq("#ReferenceNo").val() + "" : alert("Server is busy,Please try later.")
//                    }).error(function (n) {
//                        return jq.unblockUI(), alert(n.responseText), !1
//                    })
//                };
//                jq("#form_wizard_1").bootstrapWizard({
//                    nextSelector: ".button-next",
//                    previousSelector: ".button-previous",
//                    onTabClick: function () {
//                        return !1
//                    },
//                    onNext: function (u, f, e) {
//                        return i.hide(), t.hide(), 0 == n.valid() ? !1 : void r(u, f, e)
//                    },
//                    onPrevious: function (n, u, f) {
//                        i.hide();
//                        t.hide();
//                        r(n, u, f)
//                    },
//                    onTabShow: function (n, t, i) {
//                        var r = t.find("li").length,
//                            u = i + 1,
//                            f = u / r * 100;
//                        jq("#form_wizard_1").find(".progress-bar").css({
//                            width: f + "%"
//                        })
//                    }
//                });
//                jq("#form_wizard_1").find(".button-previous").hide();
//                jq("#form_wizard_1 .button-submit").click(function () { }).hide();
//                jq("#country_list", n).change(function () {
//                    n.validate().element(jq(this))
//                })
//            }
//        }
//    }
//}();