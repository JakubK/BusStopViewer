<template>
    <div>
        <h2>All stops</h2>
        <Searchbar v-model="search" placeholder="Type to search"></Searchbar>
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
import { computed, onMounted, ref } from 'vue';
import stopService from '../services/stops';
import type { Column } from 'element-plus'
import router from '../router';
import Searchbar from '../components/Searchbar.vue';
import { useStore } from '../store';
import { Stop } from '../models/stop';

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
        cellRenderer: ({ rowData }) => (<div>
            <el-button onclick={() => handleViewDelays(rowData.stopId)}>View delays</el-button>
            <el-button onclick={() => handleAssign(rowData.stopId)}>Assign</el-button>
        </div>),
    },
]

const store = useStore();
const tableData = computed(() => store.getters.getStops);

onMounted(async() => {
    const allStops = await stopService.fetchAllStops();
    store.commit('fillStops', allStops);
})

const search = ref('');
const filteredTableData = computed(() => {
    if(search.value.length === 0)
        return tableData.value;
    return tableData.value.filter((x:Stop) => {
        return x.stopName?.toLowerCase().includes(search.value.toLowerCase());
    });
});

const handleAssign = async(rowId: number) => {
    await stopService.assignStopToUser(rowId);
}

const handleViewDelays = (stopId: number) => {
    router.push(`/stops/${stopId}/delays`);
}

</script>