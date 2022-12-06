<template>
    <div>
        <h2>All stops</h2>
        <el-input v-model="search" size="small" placeholder="Type to search"></el-input>
        <el-table-v2
            :columns="columns"
            :data="filteredTableData"
            :width="700"
            :height="400"
            fixed>
        </el-table-v2>
    </div>
</template>
<script lang="tsx" setup>
import { computed, onMounted, ref, Ref } from 'vue';
import { Stop } from '../models/stop';
import stopService from '../services/stops';
import type { Column } from 'element-plus'

const columns: Partial<Column<any[]>> = [
    {
        key: 'stopId',
        dataKey: 'stopId',
        title: 'Stop Id',
        width: 120
    },
    {
        key: 'stopName',
        dataKey: 'stopName',
        title: 'Stop name',
        width: 120
    },
    {
        key: 'subName',
        dataKey: 'subName',
        title: 'Sub name',
        width: 120
    },
    {
        key: 'actions',
        title: 'Actions',
        width: 120,
        cellRenderer: ({ rowData }) => <el-button onclick={() => handleAssign(rowData.stopId)}>Assign</el-button>,
    },
]

const search = ref('');
const tableData: Ref<Stop[]> = ref([]);
onMounted(async() => {
    tableData.value = await stopService.fetchAllStops();
})

const filteredTableData = computed(() => {
    if(search.value.length === 0)
        return tableData.value;
    return tableData.value.filter(x => {
        return x.stopName?.toLowerCase().includes(search.value.toLowerCase());
    });
});

const handleAssign = async(rowId: number) => {
    await stopService.assignStopToUser(rowId);
}

const handleRemove = async(rowId: number) => {
    await stopService.removeStopFromUser(rowId);
}

</script>