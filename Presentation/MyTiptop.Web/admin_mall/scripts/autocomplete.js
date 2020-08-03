$(function () {
    //alert('@Url.Action("GetPrdtNameAjax")');
    //e.g:http://jqueryui.com/autocomplete/#multiple


    //data_45

    //$("#data_45").autocomplete(data,
    //    {
    //        formatItem: function (item)
    //        {
    //            return item.text;
    //        }
    //    }).result(function (event, item)
    //    {
    //        location.href = item.url;
    //    });



    //模具名称 智能提示. 输入框
    $("#data_45").autocomplete({
        source: function (request, response) {
            //ajax到服务器查询数据
            $.ajax({
                dataType: "json",
                type: 'post',
                url: '../Security/getPartIn',
                data: {
                    mouldCode: request.term //文本输入框的内容
                },
                success: function (data) {
                    //处理返回结果
                    response($.map(data.content, function (item) {
                        return {
                            label: item.label,
                            value: item.value
                        }
                    }));
                },
                error: function (data) {
                   // alert('智能提示出错！');
                }
            });
        },
        //输入N个字符触发智能提示
        minLength: 0
    });

});



// 下拉智能提示
$(function () {
    $.widget("custom.combobox", {
        _create: function () {
            this.wrapper = $("<span>")
              .addClass("custom-combobox") //custom-combobox
              .insertAfter(this.element);
            this.element.hide();
            this._createAutocomplete();
            this._createShowAllButton();
        },
        _createAutocomplete: function () {
            var selected = this.element.children(":selected"),
              value = selected.val() ? selected.text() : "";
            this.input = $("<input>")
              .appendTo(this.wrapper)
              .val(value)
              .attr("title", "")
              .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
              .autocomplete({
                  delay: 0,
                  minLength: 0,
                  source: $.proxy(this, "_source")
              })
              .tooltip({
                  classes: {
                      "ui-tooltip": "ui-state-highlight"
                  }
              });
            this._on(this.input, {
                autocompleteselect: function (event, ui) {
                    ui.item.option.selected = true;
                    this._trigger("select", event, {
                        item: ui.item.option
                    });
                },
                autocompletechange: "_removeIfInvalid"
            });
        },
        _createShowAllButton: function () {
            var input = this.input,
              wasOpen = false;

            $("<a>")
              .attr("tabIndex", -1)
              .attr("title", "Show All Items")
              .tooltip()
              .appendTo(this.wrapper)
              .button({
                  icons: {
                      primary: "ui-icon-triangle-1-s"
                  },
                  text: false
              })
              .removeClass("ui-corner-all")
              .addClass("custom-combobox-toggle ui-corner-right")
              .on("mousedown", function () {
                  wasOpen = input.autocomplete("widget").is(":visible");
              })
              .on("click", function () {
                  input.trigger("focus");
                  // Close if already visible
                  if (wasOpen) {
                      return;
                  }
                  // Pass empty string as value to search for, displaying all results
                  input.autocomplete("search", "");
              });
        },
        _source: function (request, response) {
            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
            response(this.element.children("option").map(function () {
                var text = $(this).text();
                if (this.value && (!request.term || matcher.test(text)))
                    return {
                        label: text,
                        value: text,
                        option: this
                    };
            }));
        },
        _removeIfInvalid: function (event, ui) {
            // Selected an item, nothing to do
            if (ui.item) {
                return;
            }
            // Search for a match (case-insensitive)
            var value = this.input.val(),
              valueLowerCase = value.toLowerCase(),
              valid = false;
            this.element.children("option").each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase) {
                    this.selected = valid = true;
                    return false;
                }
            });
            // Found a match, nothing to do
            if (valid) {
                return;
            }
            // Remove invalid value
            this.input
              .val("")
              .attr("title", value + " didn't match any item")
              .tooltip("open");
            this.element.val("");
            this._delay(function () {
                this.input.tooltip("close").attr("title", "");
            }, 2500);
            this.input.autocomplete("instance").term = "";
        },

        _destroy: function () {
            this.wrapper.remove();
            this.element.show();
        }
    });

    $(".combobox").combobox();

    //$("#toggle").on("click", function () {
    //    $(".combobox").toggle();
    //    return false;//阻止表单提交
    //});

});


