jQuery(document).ready(function ($) {

    var tableHeight = document.body.clientHeight - 238;
    var $table = $('#table_sortSearchResult');
    poTable($table, tableHeight, []);

    var cartWrapper = $('.cd-cart-container');
    var productId = 0;
    var printList = [];

    var dnnum;
    var dnnumIsExist = false;
    // 运营中心，单别
    var prefix = "", category = "";

    $('#cusdnnum').on('keyup', function () {
        var tmp_dnnum = $('#cusdnnum').val();
        if (typeof (tmp_dnnum) == 'undefined' || tmp_dnnum == null
				 || tmp_dnnum == "" || dnnum == tmp_dnnum) {
            if (tmp_dnnum == "") {
                $('#result').removeClass('visible');
                $('#result').removeClass('glyphicon-ok');
                $('#result').removeClass('result-ok');
                $('#result').removeClass('glyphicon-remove');
                $('#result').removeClass('result-remove');
            }
            return;
        }
        dnnum = "GD" + tmp_dnnum;
        $.ajax({
            type: "post",
            url: "pojson_queryDnnumIsExist",
            data: { queryType: "insert", dnnum: dnnum },
            dataType: "json",
            success: function (result) {
                // 送货单已存在
                // 登录过期
                // 送货单不存在
                if (result["status"] == 200) {
                    $('#result').removeClass('glyphicon-ok');
                    $('#result').removeClass('result-ok');
                    $('#result').addClass('glyphicon-remove');
                    $('#result').addClass('result-remove');
                    dnnumIsExist = true;
                } else if (result["status"] == 600) {
                    alert(result["message"]);
                    window.location.href = "index.jsp";
                } else {
                    $('#result').removeClass('glyphicon-remove');
                    $('#result').removeClass('result-remove');
                    $('#result').addClass('glyphicon-ok');
                    $('#result').addClass('result-ok');
                    dnnumIsExist = false;
                }
                $('#result').addClass('visible');
            },
            error: function () {
                alert("服务器响应失败");
            }
        });
    });

    if (cartWrapper.length > 0)
    {
        //store jQuery objects
        var cartBody = cartWrapper.find('.body')
        var cartList = cartBody.find('ul').eq(0);
        var cartPrint = cartWrapper.find('.checkout');
        var cartTotal = cartPrint.find('span');
        var cartTrigger = cartWrapper.children('.cd-cart-trigger');
        var cartCount = cartTrigger.children('.count')
        //var addToCartBtn = $('.cd-add-to-cart');
        var addToCartBtn = $('.add-button');
        var undo = cartWrapper.find('.undo');
        var sendDate = cartWrapper.find('#sendDate');

        var undoTimeoutId;

        //add product to cart
        addToCartBtn.on('click', function (event) {
            event.preventDefault();
            addToCart($(this));
        });

        //open/close cart
        cartTrigger.on('click', function (event) {
            event.preventDefault();
            toggleCart();
        });

        //close cart when clicking on the .cd-cart-container::before (bg layer)
        cartWrapper.on('click', function (event) {
            if ($(event.target).is($(this)))
                toggleCart(true);
        });

        //delete an item from the cart
        //cartList.on('click', '.delete-item', function(event) {
        cartList.on('click', '.fa-times', function (event) {
            event.preventDefault();
            removeProduct($(event.target).parents('.product'));
        });

        cartList.on('mouseover', function (event) {
            event.preventDefault();
            //removeProduct($(event.target).parents('.product'));
            showDeleteItem($(event.target).parents('.product'));
        });

        cartList.on('mouseout', function (event) {
            event.preventDefault();
            //removeProduct($(event.target).parents('.product'));
            hideDeleteItem($(event.target).parents('.product'));
        });

        //update item quantity
        cartList.on('change', 'select', function (event) {
            quickUpdateCart();
        });

        cartList.on('input propertychange', 'input[name="sCount"]', function (event) {
            event.preventDefault();
            setPOPmn20($(event.target).parents('.product'));//console.log($(this).val());
        });

        cartList.on('input propertychange', 'input[name="sPriceCount"]', function (event) {
            event.preventDefault();
            setPOPmn87($(event.target).parents('.product'));//console.log($(this).val());
        });

        cartPrint.on('click', function (event) {
            event.preventDefault();
            doPost();
        });

        //reinsert item deleted from the cart
        undo.on('click', 'a', function (event) {
            clearInterval(undoTimeoutId);
            event.preventDefault();
            cartList.find('.deleted').addClass('undo-deleted').one('webkitAnimationEnd oanimationend msAnimationEnd animationend', function () {
                $(this).off('webkitAnimationEnd oanimationend msAnimationEnd animationend').removeClass('deleted undo-deleted').removeAttr('style');
                quickUpdateCart();
            });
            undo.removeClass('visible');
        });

        sendDate.on('change', function (event) {
            event.preventDefault();
            setSendDate(this);
        });

        //如有显示购物车的页面，才查询缓存
        $.ajax({
            type: "post",
            url: "pojson_queryItemForCart",
            dataType: "json",
            data: { supid: $('#userid').val() },
            async: false,
            success: function (result) {
                // 成功
                if (result["status"] == 200) {
                    if (result["data"] != null && result["data"] != "") {
                        addToCart4Cache(result["data"]);
                    }
                } else if (result["status"] == 600) {
                    alert(result["message"]);
                    window.location.href = "index.jsp";
                } else {
                    alert(result["message"]);
                }
            },
            error: function () {
                alert("服务器响应失败");
            }
        });
    }

    function toggleCart(bool)
    {
        var cartIsOpen = (typeof bool === 'undefined') ? cartWrapper.hasClass('cart-open') : bool;

        if (cartIsOpen) {
            cartWrapper.removeClass('cart-open');
            //reset undo
            clearInterval(undoTimeoutId);
            undo.removeClass('visible');
            cartList.find('.deleted').remove();

            setTimeout(function () {
                cartBody.scrollTop(0);
                //check if cart empty to hide it
                if (Number(cartCount.find('li').eq(0).text()) == 0)
                    cartWrapper.addClass('empty');
            }, 500);
        } else {
            cartWrapper.addClass('cart-open');
        }
    }

    function addToCart(trigger)
    {
        var flag = false;
        var len = 0;
        var factoryInventory = document.getElementsByName("btSelectItem");
        var table = document.getElementById("table_sortSearchResult");
        var rows = null;
        if (typeof (factoryInventory) != "undefined" && typeof table !== 'undefined')
        {
            rows = table.rows;
            if (typeof (factoryInventory.length) != "undefined")
            {
                for (var i = 1; i <= factoryInventory.length; i++)
                {
                    if (factoryInventory[i - 1].checked)
                    {
                        var tmp_pmm01 = rows[i].cells[1].innerHTML; // 订单号
                        var tmp_pmn02 = rows[i].cells[2].innerHTML;	// 项次
                        var tmp_pmn33 = rows[i].cells[8].innerHTML; // 交货日期
                        var tmp_wdve = rows[i].cells[14].innerHTML; // 待交数量
                        var tmp_tc_pmb14 = rows[i].cells[17].innerHTML; // 订单状态

                        if (tmp_tc_pmb14.indexOf("未接收") > -1 || tmp_tc_pmb14.indexOf("已交货") > -1)
                        {
                            cleanup();
                            alert("订单号：" + tmp_pmm01 + " 项次：" + tmp_pmn02 + " 未接收或已完成交货。");
                            return;
                        }

                        if (tmp_wdve <= 0) {
                            cleanup();
                            alert("订单号：" + tmp_pmm01 + " 项次：" + tmp_pmn02 + " 待交数量为0，不能加入送货单。");
                            return;
                        }

                        if (!overdue(tmp_pmn33)) {
                            cleanup();
                            //prefix = "";
                            //category = "";
                            alert("订单号：" + tmp_pmm01 + " 项次：" + tmp_pmn02 + " 交货日期不能提前7天。");
                            return;
                        }

                        if (category == "")
                            category = tmp_pmm01.substring(0, 3);
                        else {
                            if (category != tmp_pmm01.substring(0, 3)) {
                                cleanup();
                                //prefix = "";
                                //category = "";
                                alert("不同单别的订单不能加入同一个送货单");
                                return;
                            }
                        }

                        if (prefix == "")
                            prefix = tmp_pmm01.substring(4, 7);
                        else
                        {
                            if (prefix != tmp_pmm01.substring(4, 7))
                            {
                                cleanup();
                                alert("不同运营中心的订单不能加入同一个送货单");
                                return;
                            }
                        }
                    }
                }

                for (var i = 1; i <= factoryInventory.length; i++) {
                    if (factoryInventory[i - 1].checked) {
                        //$('#table_sortSearchResult').each(function(){
                        var pmm01 = rows[i].cells[1].innerHTML; // 订单号
                        var pmn02 = rows[i].cells[2].innerHTML;	// 项次
                        var pmm04 = rows[i].cells[3].innerHTML;	// 采购日期
                        var gen02 = rows[i].cells[4].innerHTML;	// 采购员姓名
                        var pmn04 = rows[i].cells[5].innerHTML; // 料号
                        var pmn041 = rows[i].cells[6].innerHTML; // 品名
                        var ima021 = rows[i].cells[7].innerHTML; // 规格
                        var pmn33 = rows[i].cells[8].innerHTML; // 交货日期
                        var pmn31 = rows[i].cells[9].innerHTML; // 单价
                        var pmn07 = rows[i].cells[10].innerHTML; // 单位
                        var pmn20 = rows[i].cells[11].innerHTML; // 采购量
                        var pmn86 = rows[i].cells[12].innerHTML; // 计价单位
                        var pmn87 = rows[i].cells[13].innerHTML; // 计价数量
                        var wdve = rows[i].cells[14].innerHTML; // 待交数量
                        var pmn50 = rows[i].cells[15].innerHTML; // 已交量
                        var pmnud01 = rows[i].cells[16].innerHTML; // 备注
                        var tc_pmb14 = rows[i].cells[17].innerHTML; // 订单状态
                        var sCount, sPriceCount;
                        if (pmn07 == pmn86) {
                            sCount = wdve;
                            sPriceCount = wdve;
                        } else {
                            sCount = wdve;
                            sPriceCount = pmn87;
                        }
                        var po = {
                            "pmm01": pmm01, "pmn02": pmn02, "pmm04": pmm04, "gen02": gen02,
                            "pmn04": pmn04, "pmn041": pmn041, "ima021": ima021, "pmn33": pmn33,
                            "pmn31": pmn31, "pmn07": pmn07, "pmn20": pmn20, "pmn86": pmn86, "pmn87": pmn87,
                            "wdve": wdve, "pmn50": pmn50, "pmnud01": pmnud01, "tc_pmb14": tc_pmb14,
                            "sCount": sCount, "sPriceCount": sPriceCount, "pkgCount": seoNumber(sCount),
                            "pcsCount": seoNumber(sCount)
                        };
                        if (!IsExist(po)) {
                            if (pmn07 == "pcs" && pmn07 == pmn86) {
                                po.pmn87 = wdve;
                                addProduct(pmm01, pmn02, pmn04, pmn041, pmn07, wdve, pmn86, wdve, wdve, wdve, pmn20, "1");
                            }
                            else
                                addProduct(pmm01, pmn02, pmn04, pmn041, pmn07, wdve, pmn86, pmn87, wdve, pmn87, pmn20, "1");
                            printList.push(po);
                            cartWrapper.removeClass('empty');
                            len += 1;
                            flag = true;
                        }
                        //});
                    }
                }
                if (flag) {
                    var cartIsEmpty = cartWrapper.hasClass('empty');
                    updateCartCount(cartIsEmpty, len);
                    updateCartTotal(len, true);
                    flag = false;
                    $('#myCar').animate({ top: '65px', opacity: '1' });
                    $('#myCar').animate({ left: '1200px', opacity: '0' }, 2000);
                    $('#myCar').animate({ top: '200px', left: '150px' }, 100/*,function(){$('#myCar').animate({opacity:'0'},100)}*/);

                    addCache2Server();
                }
            }
        }

        if (printList.length == 0) {
            alert("请选择要添加到送货单数据");
            return;
        }
    }

    function cleanup() {
        if (productId === 0) {
            prefix = "";
            category = ""
        }
    }

    function addCache2Server() {
        var printListString = JSON.stringify(printList);
        $.ajax({
            type: "POST",
            url: "pojson_insertItemToCart",
            data: { supid: $('#userid').val(), poListJson: printListString },
            dataType: "json",
            success: function (result) {
                if (result["status"] == 600) {
                    alert(result["message"]);
                    window.location.href = "index.jsp";
                }
            },
            error: function (message) {
                alert("服务器响应失败");
            }
        });
    }

    function addToCart4Cache(poListString)
    {
        var flag = false;
        var len = 0;
        var factoryInventory = document.getElementsByName("btSelectItem");

        var poList = eval('(' + poListString + ')');

        for (var i = 0; i < poList.length; i++) {
            if (poList[i].pmn07 == "pcs" && poList[i].pmn07 == poList[i].pmn86) {
                poList[i].pmn87 = poList[i].wdve;
                addProduct(poList[i].pmm01, poList[i].pmn02, poList[i].pmn04, poList[i].pmn041,
						   poList[i].pmn07, poList[i].wdve, poList[i].pmn86, poList[i].wdve,
						   poList[i].sCount, poList[i].sPriceCount, poList[i].pmn20, "2");
            }
            else
                addProduct(poList[i].pmm01, poList[i].pmn02, poList[i].pmn04, poList[i].pmn041,
						   poList[i].pmn07, poList[i].wdve, poList[i].pmn86, poList[i].pmn87,
						   poList[i].sCount, poList[i].sPriceCount, poList[i].pmn20, "2");
            printList.push(poList[i]);

            if (category == "")
                category = poList[i].pmm01.substring(0, 3);
            if (prefix == "")
                prefix = poList[i].pmm01.substring(4, 7);

            cartWrapper.removeClass('empty');
            len += 1;
            flag = true;
        }
        if (flag) {
            var cartIsEmpty = cartWrapper.hasClass('empty');
            updateCartCount(cartIsEmpty, len);
            updateCartTotal(len, true);
            flag = false;

        }
    }

    //交货日期提前7天
    function overdue(d) {

        var date = new Date(d);

        var m = date.getTime() - 7 * 24 * 3600 * 1000;

        var d = new Date(m);

        var current = new Date();

        if (current >= d) return true;
        else return false;
    }

    function IsExist(po)
    {
        if (printList.length == 0) return false;
        for (var i = 0; i < printList.length; i++) {
            if (po.pmm01 == printList[i].pmm01 && po.pmn02 == printList[i].pmn02) {
                return true;
            }
        }
        return false;
    }

    function addProduct(pmm01, pmn02, pmn04, pmn041, pmn07, pmn20, pmn86, pmn87, sCount, sPriceCount, sum, start)
    {
        productId = productId + 1;
        var select = '', productAdded = '';
        //访问ajax
        var qtydlvobj = { pmm01: pmm01, pmn02: pmn02 };
        var qtydlvjson = JSON.stringify(qtydlvobj);
        $.ajax({
            type: "post",
            traditional: true,// 这使json格式的字符不会被转码
            url: "pojson_queryQtyDlv",
            data: {
                "qtydlv": qtydlvjson
            },
            cache: false,
            dataType: "json",
            success: function (qty) {
                if (qty == null) {
                    qty = 0;
                }
                sCount = parseInt(sum) - parseInt(qty);
                sPriceCount = sCount;
                if (start == "1") {
                    for (var i = 0; i < printList.length; i++) {
                        if (pmm01 == printList[i].pmm01 && pmn02 == printList[i].pmn02) {
                            printList[i].sCount = sCount;
                            printList[i].wdve = sCount;
                            printList[i].pcsCount = sCount;
                            printList[i].pkgCount = sCount;
                        }
                    }
                }

                var productAdded = $('<li class="product">'
						+ '<div style="position: absolute; width: 180px; max-height: 110px; top: 0; left: 0;">'
						+ '<strong>单号：</strong><h5 data-item="' + pmn02 + '" style="display:inline">' + pmm01 + '</h5>'
						+ '<strong>项次：</strong><span class="price">' + pmn02 + '</span>'
						+ '</div>'
						+ '<div style="width: auto; min-height: 65px; max-height: 140px; margin: 0 100px 0 180px; padding: 0 5px;">'
						+ '<strong>料号：</strong><span class="price">' + pmn04 + '</span>'
						+ '<p>' + pmn041 + '</p>'
						+ '</div>'
						+ '<div class="quantity actions" style="position: absolute; top: 0; right: -10; width:150px; max-height: 100px;">'
						+ '<label  style="font-size:3px">送货数量</label>'
						+ '<input name="sCount" type="text" value="' + seoNumber(sCount) + '" text="' + seoNumber(pmn20) + '" class="form-control" style="padding: 6px;width: 56px;height: 30px;display:inline;">'
						+ '<em>' + pmn07 + '</em>'
						+ '<label  style="font-size:3px">计价数量</label>'
						+ '<input name="sPriceCount" type="text" value="' + seoNumber(sPriceCount) + '" text="' + seoNumber(pmn87) + '" class="form-control" style="padding: 6px;width: 56px;height: 30px;display:inline;margin-top: 2px;">' //border-color: #FF6100
						+ '<em>' + pmn86 + '</em>'
						+ '</div>'
						+ '<span class="delete-area" style="position: absolute;top: 0;right: 0;background-color: #f00;border-radius: 0 0 0 18px;height: 18px;width: 18px;"></span>'
						+ '<i class="fa fa-times delete-area" aria-hidden="true" style="position: absolute;top: 0;right: 0"></i>'
                        + '</li>');
                cartList.prepend(productAdded);
                hideDeleteItem(cartList);
            }
        })


    }

    function setSendDate(target)
    {
        //console.log(target);

        var current = new Date();
        var td = new Date(target.value);
        if (td.getFullYear() < current.getFullYear() ||
				td.getMonth() < current.getMonth() ||
				td.getDate() < current.getDate()) {
            alert("送货日期不能小于当前日期");
            target.value = current.getFullYear() + "-" + (current.getMonth() + 1) + "-" + current.getDate();
            return;
        }

        if (!overdue(target.value)) {
            alert("送货单不能提前7天打印");
            var time = current.getTime() + 7 * 24 * 3600 * 1000;
            var bd = new Date(time);
            target.value = bd.getFullYear() + "-" + (bd.getMonth() + 1) + "-" + bd.getDate();
        }
    }

    function setPOPmn20(product)
    {
        var pmm01 = product.find('h5').text(),
			pmn02 = product.find('h5').attr('data-item'),
			sCount = parseInt(product.find('input[name="sCount"]').val()),
			max = parseInt(product.find('input[name="sCount"]').attr('text'));
        if (typeof wdve === 'number' && isNaN(sCount)) { alert('请输入数字'); return; }
        for (var i = 0; i < printList.length; i++) {
            if (pmm01 == printList[i].pmm01 && pmn02 == printList[i].pmn02) {
                if (sCount <= 0) {
                    product.find('input[name="sCount"]').val(1);
                    if (printList[i].pmn07 == printList[i].pmn86) {
                        product.find('input[name="sPriceCount"]').val(1);
                        printList[i].sCount = 1;
                        printList[i].sPriceCount = 1;
                    }
                    alert('数量超出了范围');
                    break;
                }
                if (sCount > max) {
                    product.find('input[name="sCount"]').val(max);
                    if (printList[i].pmn07 == printList[i].pmn86) {
                        product.find('input[name="sPriceCount"]').val(max);
                        printList[i].sCount = max;
                        printList[i].sPriceCount = max;
                    }
                    alert('数量超出了范围');
                    break;
                }

                if (printList[i].pmn07 == printList[i].pmn86) {
                    product.find('input[name="sPriceCount"]').val(sCount);
                    printList[i].sPriceCount = sCount;
                }
                printList[i].sCount = sCount;
                break;
            }
        }
        addCache2Server();
    }

    function setPOPmn87(product)
    {
        var pmm01 = product.find('h5').text(),
			pmn02 = product.find('h5').attr('data-item'),
			sPriceCount = parseFloat(product.find('input[name="sPriceCount"]').val()),
			max = parseFloat(product.find('input[name="sPriceCount"]').attr('text'));
        if (typeof pmn87 === 'number' && isNaN(sPriceCount)) { alert('请输入数字'); return; }
        for (var i = 0; i < printList.length; i++) {
            if (pmm01 == printList[i].pmm01 && pmn02 == printList[i].pmn02) {
                if (sPriceCount <= 0) {
                    product.find('input[name="sPriceCount"]').val(1);
                    alert('数量必须要大于0');
                    break;
                }
                /*if(pmn87 > max) {
					product.find('input[name="pmn87"]').val(max);
					alert('数量超出了范围');
					break;
				}*/

                printList[i].sPriceCount = sPriceCount;
                break;
            }
        }
        addCache2Server();
    }

    function removeProduct(product) {
        productId = productId - 1;

        clearInterval(undoTimeoutId);
        cartList.find('.deleted').remove();

        var topPosition = product.offset().top - cartBody.children('ul').offset().top,
			pmm01 = product.find('h5').text(),
			pmn02 = product.find('h5').attr('data-item');
        for (var i = 0; i < printList.length; i++) {
            if (pmm01 == printList[i].pmm01 && pmn02 == printList[i].pmn02) {
                printList.splice(i, 1);
                break;
            }
        }
        product.css('top', topPosition + 'px').addClass('deleted');
        updateCartCount(true, -1);
        updateCartTotal(-1, true);

        undo.addClass('visible');

        addCache2Server();

        //wait 8sec before completely remove the item
        undoTimeoutId = setTimeout(function () {
            //undo.removeClass('visible');
            cartList.find('.deleted').remove();
        }, 1000);
    }

    function quickUpdateCart() {
        var quantity = 0;
        var price = 0;

        cartList.children('li:not(.deleted)').each(function () {
            var singleQuantity = Number($(this).find('.select').find('i').text());
            quantity = quantity + singleQuantity;
            price = price + singleQuantity * Number($(this).find('.price').text().replace('￥', ''));
        });

        cartTotal.text(price.toFixed(2));
        cartCount.find('li').eq(0).text(quantity);
        cartCount.find('li').eq(1).text(quantity + 1);
    }

    function updateCartCount(emptyCart, next)
    {


        var actual = Number(cartCount.find('li').eq(0).text()) + next;
        //var next = actual + 1;
        if (emptyCart) {
            cartCount.find('li').eq(0).text(actual);
            cartCount.find('li').eq(1).text(next);

            if (actual == 0) prefix = "";
        } else {
            cartCount.addClass('update-count');

            setTimeout(function () {
                cartCount.find('li').eq(0).text(actual);
            }, 150);

            setTimeout(function () {
                cartCount.removeClass('update-count');
            }, 200);

            setTimeout(function () {
                cartCount.find('li').eq(1).text(next);
            }, 230);
        }




 
    }

    function updateCartTotal(price, bool) {
        var total = Number(cartTotal.text()) + Number(price);
        if (total == 0) {
            prefix = "";
            category = "";
        }
        bool ? cartTotal.text((Number(cartTotal.text()) + Number(price))) : cartTotal.text((Number(cartTotal.text()) - Number(price)));
    }

    function checkPOOutPrintCount()
    {
        var returnValue = true;
        $.ajax({
            type: "POST",
            url: "pojson_queryPOOutOfPrintCount",
            dataType: 'json',
            data: { "poListJson": JSON.stringify(printList) },
            success: function (res) {
                if (res["status"] == 400) {
                    var str = "";
                    var list = res["po"];
                    for (var i = 0; i < list.length; i++) {
                        var index = list[i].indexOf("&");
                        str += "单号：" + list[i].substring(0, index - 1) + " 项次：" + list[i].substring(index);
                    }
                    str += "/n总打印数量超出了订单数量。";
                    alert(str);
                    returnValue = false;
                }
            },
            error: function () {
                alert("服务器响应失败");
                returnValue = false;
            }
        });

        return returnValue;
    }

    function doPost(supid)
    {  // to:提交动作（action）,p:参数  

        $.ajax({
            type: "POST",
            url: "pojson_queryPOOutOfPrintCount",
            dataType: 'json',
            data: { "poListJson": JSON.stringify(printList) },
            success: function (result) {
                if (result["status"] == 400) {
                    var str = "";
                    var list = result["data"];
                    for (var i = 0; i < list.length; i++) {
                        var index = list[i].indexOf("&");
                        str += "单号：" + list[i].substring(0, index) + " 项次：" + list[i].substring(index + 1);
                        str += "\n";
                    }
                    str += "\n总打印数量超出了订单数量，请核实。";
                    alert(str);
                    returnValue = false;
                } else if (result["status"] == 600) {
                    alert(result["message"]);
                    window.location.href = "index.jsp";
                } else {
                    if (dnnumIsExist) return;
                    if (printList.length == 0) return;
                    $.post("pojson_deleteItemForCart", { supid: $('#userid').val() }, function (result) {
                        if (result["status"] == 600) {
                            alert(result["message"]);
                            window.location.href = "index.jsp";
                        }
                    });
                    var myForm = document.createElement("form");
                    myForm.method = "post";
                    myForm.action = "po_toPrintList";
                    var myInput = document.createElement("input");
                    myInput.name = "poListJson";
                    myInput.value = JSON.stringify(printList);
                    var myInput2 = document.createElement("input");
                    myInput2.name = "dnnum";
                    var tmp_dnnum = $('#cusdnnum').val().replace(/\s/g, "");
                    if (tmp_dnnum != "") tmp_dnnum = "GD" + tmp_dnnum;
                    myInput2.value = tmp_dnnum;
                    /*var myInput3 = document.createElement("input");
	    	        myInput3.name = "userid";
	    	        myInput3.value = $('#userid').val();*/
                    var myInput4 = document.createElement("input");
                    myInput4.name = "supid";
                    myInput4.value = $('#userid').val();
                    var myInput5 = document.createElement("input");
                    myInput5.name = "sendDate";
                    myInput5.value = $('#sendDate').val();
                    myForm.appendChild(myInput);
                    myForm.appendChild(myInput2);
                    //myForm.appendChild(myInput3);
                    myForm.appendChild(myInput4);
                    myForm.appendChild(myInput5);
                    document.body.appendChild(myForm);
                    myForm.submit();
                    document.body.removeChild(myForm);  // 提交后移除创建的form 
                }
            },
            error: function () {
                alert("服务器响应失败");
                returnValue = false;
            }
        });
    }



    function showDeleteItem(product) {
        product.css("background-color", "#eee");
        product.find(".delete-area").show();
    }

    function hideDeleteItem(product) {
        product.css("background-color", "#fff");
        product.find(".delete-area").hide();
    }

    function seoNumber(number) {
        var nInt = parseInt(number);
        return parseFloat(nInt) == number ? nInt : number;
    }
});