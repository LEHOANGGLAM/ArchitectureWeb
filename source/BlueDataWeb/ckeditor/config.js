/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
    // Define changes to default configuration here. For example:
    //config.uiColor = '#EDEDED';
    config.enterMode = CKEDITOR.ENTER_BR;
    config.filebrowserBrowseUrl = '/ckeditor/ckfinder/ckfinder.html?Type=Files';
    config.filebrowserFileBrowseUrl = '/ckeditor/ckfinder/ckfinder.html?Type=Files';
    config.filebrowserImageBrowseUrl = '/ckeditor/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/ckeditor/ckfinder/ckfinder.html?Type=Flash';
   // config.htmlEncodeOutput = true; / fix bug html
   // config.fillEmptyBlocks = false;
    config.language = 'vi';
    //config.skin = 'kama';
    config.entities = false;
    //config.extraPlugins = 'tinmedia';
    config.font_names = 'Times New Roman;Arial;Verdana;Tahoma;Calibri;Georgia;Courier New';
    //config.format_tags = 'p;h1;h2;h3;h4;h5;h6;pre;address;div';

    config.filebrowserUploadUrl = '/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserFileUploadUrl = '/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    config.toolbar = 'CustomFull';
    config.toolbar_CustomFull =
    [
	    { name: 'document', items: ['Source', 'Maximize', '-', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', '-', 'Templates'] },
	    { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
	    { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'HorizontalRule', 'SpecialChar', 'PageBreak', 'Blockquote', 'TextColor', 'BGColor', 'Styles', 'FontSize'] },
	    '/',
	    { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
	    { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
	    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
	    { name: 'insert', items: ['Image', 'Flash', 'TinMedia', 'Table', 'Smiley', 'Format', 'Font'] }
    ];
    config.toolbar_CustomBasic =
    [
	    ['Source', 'Bold', 'Italic', 'PasteText', 'PasteFromWord', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'Link', 'Unlink', '-', 'TextColor', 'Undo', 'Redo']
    ];
};
