$('form').submit(function () {

    var strTextoPesquisa = $("#strTextoPesquisa").val();
    var strFiltro = "";
    if ($("#strFiltro_0").is(":checked")) {
        strFiltro = $("#strFiltro_0").val();
    } else if ($("#strFiltro_1").is(":checked")) {
        strFiltro = $("#strFiltro_1").val();
    } else if ($("#strFiltro_2").is(":checked")) {
        strFiltro = $("#strFiltro_2").val();
    } else {
        strFiltro = $("#strFiltro_0").val();
    }

    if (strTextoPesquisa != "") {
        $('#jqgridListaPesquisa').jqGrid({
            url: "handler/HandlerTurma.ashx?strAcao=L",
            datatype: "json",
            colNames: ["ID", "Descrição"],
            colModel: [
        { name: "TurId", index: "TurId", sorttype: "int", editable: false, hidden: true },
        { name: "TurDescricao", index: "TurDescricao", sorttype: "string", editable: true }
        ],
            rowNum: 10,
            height: 200,
            pager: "#pagerJqgridListaPesquisa",
            viewrecords: true,
            autowidth: true,
            sortname: "TurId",
            sortorder: 'asc'
        });
    }
});