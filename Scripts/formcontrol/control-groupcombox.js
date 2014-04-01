﻿(function ($) {
  $.extend($.fn, {
    groupCombox:
    function (setting) {
      if (!setting) {
        setting = {};
      }
      var ps = $.extend({
        valueField: 'value',
        textField: 'text',
        groupField: 'group',
        method: 'get',
        multiple: false,
        editable: false,
        width: 186,
        url: ''
      }, setting);
      var that = this;
      var id = that.attr('id');
      var comboxId = id + '_Combox';
      function binding() {
        createCombox();
      }

      function createCombox() {
        var control = $('<input />').attr('id', comboxId)
        that.after(control);
        var opts = $.extend(ps, {
          onChange: onChange
        })
        control.combobox(ps);
      }
      function onChange(newvalue, oldvalue) {
        //var value = newvalue.concat(newvalue);
        //value = _.uniq(value);
        that.val(newvalue.join(","));
        that.parents('form:first').validate().element('#' + id);
      }

      binding();
    }
  })
})(jQuery)